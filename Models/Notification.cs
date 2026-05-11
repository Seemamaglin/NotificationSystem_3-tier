namespace NotificationSystem_3_tier.Models
{
    public class Notification
    {   
        public int Id {get; set;}
        public string Message{get; set;}
        public string NotificationType{get; set;}
        public DateTime SentDate {get; set;}
        public string Status {get; set;}
        public string RecipientName {get; set;}

        public string RecipientContact {get; set;}

        public Notification(int id, string message,string notificationType, DateTime sentDate, string status, 
                    string recipientName, string recipientContact)
        {
            Id = id;
            Message = message;
            NotificationType = notificationType;
            SentDate = sentDate;
            Status = "Pending";
            RecipientName = recipientName;
            RecipientContact = recipientContact;
        }

    }
}