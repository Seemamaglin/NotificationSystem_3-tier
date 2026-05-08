using Notification_System_CRUD.Models;
using Notification_System_CRUD.Interfaces;
using Notification_System_CRUD.Repositories;

namespace Notification_System_CRUD.Interfaces
{
    public interface INotificationSender
    {
        void Send(User user, Notification notification);
    }
}