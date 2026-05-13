using Microsoft.EntityFrameworkCore;
using System;
using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.NotificationSenders;

namespace NotificationSystem_3_tier.Contexts
{
    public class NotificationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Notification_Db;Username=postgres;Password=Lovlin@2004");
        }

        public DbSet<User> users {get; set;}
        public DbSet<Notification> notifications {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.HasKey(u => u.Id);
                u.Property(u => u.PhoneNumber).HasColumnName("phone");
            });

            modelBuilder.Entity<Notification>(n=>
            {
               n.HasKey(n => n.Id);
               n.Property(n => n.SentDate).HasColumnType("timestamp without time zone");
               n.Property(n => n.NotificationType).HasColumnName("notification_type");
               n.Property(n => n.SentDate).HasColumnName("sent_date");
               n.Property(n => n.RecipientName).HasColumnName("recipient_name");
               n.Property(n => n.RecipientContact).HasColumnName("recipient_contact");

               n.HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .HasConstraintName("FK_Notification_User")
                .OnDelete(DeleteBehavior.Restrict);

                n.Property(n => n.UserId).HasColumnName("user_id");
            });
        }
    }
}