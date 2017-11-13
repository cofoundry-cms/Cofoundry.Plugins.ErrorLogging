using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Domain;
using Cofoundry.Plugins.ErrorLogging.Data;
using Microsoft.EntityFrameworkCore;

namespace Cofoundry.Plugins.ErrorLogging.Domain
{
    public class GetErrorDetailsByIdQueryHandler 
        : IAsyncQueryHandler<GetByIdQuery<ErrorDetails>, ErrorDetails>
        , IPermissionRestrictedQueryHandler<GetByIdQuery<ErrorDetails>, ErrorDetails>
    {
        private readonly ErrorLoggingDbContext _dbContext;

        public GetErrorDetailsByIdQueryHandler(
            ErrorLoggingDbContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task<ErrorDetails> ExecuteAsync(GetByIdQuery<ErrorDetails> query, IExecutionContext executionContext)
        {
            var error = await _dbContext
                .Errors
                .AsNoTracking()
                .Where(u => u.ErrorId == query.Id)
                .Select(e => new ErrorDetails()
                {
                    CreateDate = e.CreateDate,
                    Data = e.Data,
                    EmailSent = e.EmailSent,
                    ErrorId = e.ErrorId,
                    ExceptionType = e.ExceptionType,
                    Form = e.Form,
                    QueryString = e.QueryString,
                    Session = e.QueryString,
                    Source = e.Source,
                    StackTrace = e.QueryString,
                    Target = e.Target,
                    Url = e.Url,
                    UserAgent = e.UserAgent
                })
                .SingleOrDefaultAsync();

            return error;
        }

        #region Permission

        public IEnumerable<IPermissionApplication> GetPermissions(GetByIdQuery<ErrorDetails> query)
        {
            yield return new ErrorLogReadPermission();
        }

        #endregion
    }
}
