namespace Cofoundry.Plugins.ErrorLogging.Domain;

public class ErrorDetails
{
    public int ErrorId { get; set; }
    public string ExceptionType { get; set; }
    public string Url { get; set; }
    public string Source { get; set; }
    public string Target { get; set; }
    public string StackTrace { get; set; }
    public string QueryString { get; set; }
    public string Session { get; set; }
    public string Form { get; set; }
    public string Data { get; set; }
    public string UserAgent { get; set; }
    public bool EmailSent { get; set; }
    public System.DateTime CreateDate { get; set; }
}
