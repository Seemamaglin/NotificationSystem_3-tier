using Notification_System_CRUD.Interfaces;
using Notification_System_CRUD.Models;

namespace Notification_System_CRUD.NotificationSenders
{
    // Presentation of an email being dispatched (console simulation).
    public class EmailNotificationSender : INotificationSender
    {
        public void Send(User user, Notification notification)
        {
            notification.Status          = "Sent";
            notification.RecipientContact = user.Email;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n  [EMAIL NOTIFICATION]");
            Console.ResetColor();
            Console.WriteLine($"  To      : {user.Email}");
            Console.WriteLine($"  Name    : {user.Name}");
            Console.WriteLine($"  Message : {notification.Message}");
            Console.WriteLine($"  Sent At : {notification.SentDate:dd-MM-yyyy HH:mm:ss}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  Status  : {notification.Status}");
            Console.ResetColor();
        }
    }
}
