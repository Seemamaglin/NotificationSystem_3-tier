using Notification_System_CRUD.Services;
using Notification_System_CRUD.Interfaces;

namespace Notification_system_CRUD.Models
{
    
}namespace Notification_System_CRUD.Models
{// Thrown when the notification message is null or empty
    public class EmptyMessageException : Exception
    {
        public EmptyMessageException()
            : base("Message cannot be empty.") { }

        public EmptyMessageException(string message)
            : base(message) { }
    }

    public class MessageTooShortException : Exception
    {
        public MessageTooShortException()
            : base("Message must be at least 5 characters long.") { }

        public MessageTooShortException(string message)
            : base(message) { }
    }
    
    public class SmsTooLongException : Exception
    {
        public SmsTooLongException(int actualLength)
            : base($"SMS message is too long ({actualLength} characters). Maximum allowed is 160.") { }
    }
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string email)
            : base($"'{email}' is not a valid email address. Example: name@example.com") { }
    }

    public class InvalidPhoneException : Exception
    {
        public InvalidPhoneException(string phone)
            : base($"'{phone}' is not a valid phone number. Use digits only (min 7 digits).") { }
    }

    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(int id)
            : base($"A user with ID {id} already exists. Please choose a different ID.") { }
    }
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int id)
            : base($"No user found with ID {id}.") { }
    }
    public class InvalidNotificationTypeException : Exception
    {
        public InvalidNotificationTypeException(string type)
            : base($"'{type}' is not a supported notification type. Use 'Email' or 'SMS'.") { }
    }
}