namespace Cofoundry.Plugins.ErrorLogging.Domain;

public class ErrorSummary
{
    public int ErrorId { get; set; }
    public string ExceptionType { get; set; }
    public string Url { get; set; }
    public string UserAgent { get; set; }
    public System.DateTime CreateDate { get; set; }
}
