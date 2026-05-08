using Notification_System_CRUD.Models;
using Notification_System_CRUD.Interfaces;

namespace Notification_System_CRUD.Repositories
{
    public class NotificationRepository
    {
        private readonly List<Notification> notifications=new();
        private int newId=1;

        public void Save(Notification notification)
        {
            notification.Id = newId++;
            notifications.Add(notification);
        }
        public List<Notification> GetAll()
        {
            return notifications;
        }

    }
}