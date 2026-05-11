using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;

namespace NotificationSystem_3_tier.Repositories
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