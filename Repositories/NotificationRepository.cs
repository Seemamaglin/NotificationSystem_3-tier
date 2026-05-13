using Microsoft.EntityFrameworkCore;
using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Contexts;
using Npgsql;

namespace NotificationSystem_3_tier.Repositories
{
    public class NotificationRepository
    {
        NotificationContext context;

        public NotificationRepository()
        {
            context=new NotificationContext();
        }

        //Save notification of a user
        //ADO :INSERT SQL with 7 parameters +Execute scalar
        // EF Core :set FK as UserId, use .Add() and then save changes()

        public void Save(Notification n, int userId)
        {
            //Link the notification to user via FK
            n.UserId=userId;

            context.notifications.Add(n);
            Console.WriteLine("State before SaveChanges: " + context.Entry<Notification>(n).State);
            context.SaveChanges();
            Console.WriteLine("State after SaveChanges: " + context.Entry<Notification>(n).State);

        }

        //Get all user notifications of the linked user

        public List<Notification> GetAll()
        {
            return context.notifications
                    .Include(n => n.User)
                    .OrderByDescending(n => n.SentDate)
                    .ToList();
        }
    }
}