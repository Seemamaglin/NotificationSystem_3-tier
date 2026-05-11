using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using NotificationSystem_3_tier.Repositories;
using NotificationSystem_3_tier.NotificationSenders;

namespace NotificationSystem_3_tier.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _repository;


        public NotificationService(NotificationRepository repository)
        {
            _repository=repository;
        }

        public void SendNotification(User user,string message,string notificationType)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new EmptyMessageException();

            if (message.Trim().Length <5)
                throw new MessageTooShortException();

            if (notificationType == "Email")
            {
                if (!IsValidEmail(user.Email))
                    throw new InvalidEmailException(user.Email);
            }
            else if (notificationType == "SMS")
            {
                if (!IsValidPhone(user.PhoneNumber))
                    throw new InvalidPhoneException(user.PhoneNumber);

                if (message.Length > 160)
                    throw new SmsTooLongException(message.Length);
            }
            else
            {
                throw new InvalidNotificationTypeException(notificationType);
            }

            var notification = new Notification(
                id: 0,
                message: message,
                notificationType: notificationType,
                sentDate: DateTime.Now,
                recipientName: user.Name,
                recipientContact: string.Empty,
                status: "Pending"
            );

            INotificationSender sender = notificationType switch
            {
                "Email"=> new EmailNotificationSender(),
                "SMS" => new SmsNotificationSender(),
                _  => throw new InvalidNotificationTypeException(notificationType)
            };
            sender.Send(user, notification);

            _repository.Save(notification,user.Id);
        }

        public List<Notification> GetSentNotifications()
        {
            return _repository.GetAll();
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            int atIndex  = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');
            return atIndex > 0 && dotIndex > atIndex + 1 && dotIndex < email.Length - 1;
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            int digitCount = 0;
            foreach (char c in phone)
            {
                if (char.IsDigit(c)) { digitCount++; continue; }
                if (c == '+' || c == '-' || c == ' ') continue;
                return false;
            }
            return digitCount >= 7;
        }
    }

}