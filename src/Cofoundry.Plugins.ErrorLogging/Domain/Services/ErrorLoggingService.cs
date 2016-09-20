using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using Cofoundry.Domain.CQS;
using Cofoundry.Core.Configuration;
using Cofoundry.Core.Mail;
using Cofoundry.Core.ErrorLogging;
using Cofoundry.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class ErrorLoggingService : IErrorLoggingService
    {
        private readonly IMailDispatchService _mailService;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ErrorLoggingDbContext _dbContext;
        private readonly IConfigurationService _configurationService;
        private readonly ErrorLoggingSettings _errorLoggingSettings;

        #region constructor

        public ErrorLoggingService(
            IQueryExecutor queryExecutor,
            IConfigurationService configurationService,
            IMailDispatchService mailService,
            ErrorLoggingDbContext dbContext,
            ErrorLoggingSettings errorLoggingSettings
            )
        {
            _queryExecutor = queryExecutor;
            _mailService = mailService;
            _dbContext = dbContext;
            _configurationService = configurationService;
            _errorLoggingSettings = errorLoggingSettings;
        }

        #endregion

        #region public methods

        public void Log(Exception ex)
        {
            var inner = ex.GetBaseException() ?? ex;

            var error = MapError(inner);
            string additionalText;
            Exception dbLoggingException = null;

            try
            {
                _dbContext.Errors.Add(error);
                _dbContext.SaveChanges();
                additionalText = "\r\n\r\nSaved in DB: Yes";
            }
            catch (Exception loggingEx)
            {
                dbLoggingException = loggingEx;
                additionalText = String.Format("\r\n\r\nSaved in DB: No\r\n\r\nException:{0}", loggingEx.Message);
            }

            if (!string.IsNullOrWhiteSpace(_errorLoggingSettings.LogToEmailAddress))
            {
                try
                {
                    SendMail(error, additionalText);
                    error.EmailSent = true;
                    _dbContext.SaveChanges();
                }
                catch
                {
                }
            }
        }

        public void LogWarning(Exception ex)
        {
            // TODO: Implement warning category
            Log(ex);
        }

        #endregion

        #region private helpers

        private Error MapError(Exception ex)
        {
            Error error = new Error
            {
                Source = ex.Source ?? string.Empty,
                ExceptionType = ex.Message ?? string.Empty,
                Target = Convert.ToString(ex.TargetSite),
                StackTrace = ex.StackTrace ?? string.Empty,
                EmailSent = false,
                CreateDate = DateTime.UtcNow
            };

            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                error.Url = HttpContext.Current.Request.Url.AbsoluteUri;
                error.QueryString = HttpContext.Current.Request.QueryString.ToString();
                error.Session = "";
                error.Form = HttpContext.Current.Request.Unvalidated.Form.ToString();
                error.UserAgent = HttpContext.Current.Request.UserAgent.ToString();

                if (HttpContext.Current.Session != null)
                {
                    foreach (var sessionItem in HttpContext.Current.Session)
                    {
                        error.Session += sessionItem.ToString() + " ";
                    }
                }
            }

            if (ex.Data != null && ex.Data.Count > 0)
            {
                error.Data = string.Join(", ", ex.Data);
            }
            return error;
        }

        private void SendMail(Error error, string additionalText)
        {
            var hasRequest = HttpContext.Current != null && HttpContext.Current.Request != null;
            var user = _queryExecutor.Execute(new GetCurrentUserMicroSummaryQuery());

            string bodyText = String.Format("Url: {0}\r\n\r\nUser: {1}\r\n\r\nException: {2}\r\n\r\nTarget site: {3}\r\n\r\nStack trace: {4}",
                hasRequest ? HttpContext.Current.Request.Url.AbsoluteUri : string.Empty,
                user == null ? "Unauthenticated" : String.Format("{0}: {1}", user?.UserArea.Name, user.Email),
                error.ExceptionType,
                error.Target,
                error.StackTrace);


            if (hasRequest)
            {
                var form = HttpContext.Current.Request.Unvalidated.Form;

                bodyText = String.Format("{0}\r\n\r\nQuery string: {1}", bodyText, error.QueryString);

                if (HttpContext.Current.Session.Keys.Count > 0)
                {
                    bodyText = String.Format("{0}\r\n\r\nSession:\r\n", bodyText);

                    for (int i = 0; i < HttpContext.Current.Session.Keys.Count; i++)
                    {
                        bodyText = String.Format("{0}\r\n{1}", bodyText, HttpContext.Current.Session.Keys[i].ToString());
                    }
                }

                if (form.Keys.Count > 0)
                {
                    bodyText = String.Format("{0}\r\n\r\nForm:\r\n", bodyText);

                    foreach (string key in form.Keys)
                    {
                        bodyText = String.Format("{0}\r\n{1}: {2}", bodyText, key, form.GetValues(key).Aggregate("", (a, b) => (a + "," + b)));
                    }
                }

                if (!String.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
                {
                    bodyText = String.Format("{0}\r\n\r\nUser agent:{1}", bodyText, HttpContext.Current.Request.UserAgent.ToString());
                }
            }

            if (error.Data != null)
            {
                bodyText = String.Format("{0}\r\n\r\nData: {1}", bodyText, error.Data);
            }

            bodyText += additionalText;

            var msg = new MailMessage();
            msg.TextBody = bodyText;
            msg.To = new SerializeableMailAddress(_errorLoggingSettings.LogToEmailAddress);
            msg.Subject = "Exception: " + error.Source;

            if (!Debugger.IsAttached)
            {
                _mailService.Dispatch(msg);
            }
        }

        #endregion
    }
}
