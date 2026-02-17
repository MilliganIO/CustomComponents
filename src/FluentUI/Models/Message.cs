namespace FluentUI.Models;

public class Message
{
    public string Sender { get; set; } = string.Empty;
    public string Recepient { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsImportant { get; set; }
    public DateTime SentDate { get; set; } = DateTime.Now.AddDays(-1);
}
