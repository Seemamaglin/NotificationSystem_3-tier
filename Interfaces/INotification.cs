
using Notification_System_CRUD.Models;
namespace Notification_System_CRUD.Interfaces
{
    public interface INotification
    {
        string Message { get; set; }
        DateTime SentDate { get; set; }
        string Status {get; set; }
        void Send(User user);
    }
}