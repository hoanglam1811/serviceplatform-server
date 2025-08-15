using Microsoft.AspNetCore.Http;

public class SendEmailRequest
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public List<IFormFile>? File { get; set; }
    public string UserName { get; set; }
}
