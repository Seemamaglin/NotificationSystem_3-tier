using NotificationSystem_3_tier.Models;
namespace NotificationSystem_3_tier.Interfaces
{
    public interface INotification
    {
        string Message { get; set; }
        DateTime SentDate { get; set; }
        string Status {get; set; }
        void Send(User user);
    }
}