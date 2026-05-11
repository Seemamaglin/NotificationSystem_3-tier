using NotificationSystem_3_tier.Interfaces;
using NotificationSystem_3_tier.Models;

namespace NotificationSystem_3_tier.NotificationSenders
{
    // Presentation of an SMS being dispatched (console simulation).
    public class SmsNotificationSender : INotificationSender
    {
        public void Send(User user, Notification notification)
        {
            notification.Status           = "Sent";
            notification.RecipientContact = user.PhoneNumber;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  [SMS NOTIFICATION]");
            Console.ResetColor();
            Console.WriteLine($"  To      : {user.PhoneNumber}");
            Console.WriteLine($"  Name    : {user.Name}");
            Console.WriteLine($"  Message : {notification.Message}");
            Console.WriteLine($"  Sent At : {notification.SentDate:dd-MM-yyyy HH:mm:ss}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Status  : {notification.Status}");
            Console.ResetColor();
        }
    }
}
