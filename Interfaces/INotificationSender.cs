using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using NotificationSystem_3_tier.Repositories;

namespace NotificationSystem_3_tier.Interfaces
{
    public interface INotificationSender
    {
        void Send(User user, Notification notification);
    }
}