using NotificationSystem_3_tier.Models;
using Npgsql;

namespace NotificationSystem_3_tier.Repositories
{
    public class NotificationRepository
    {
        string connectionString = "Host=localhost;Port=5433;Database=Notification_DB;Username=postgres;Password=Lovlin@2004";
        NpgsqlConnection connection;

        public NotificationRepository()
        {
            connection = new NpgsqlConnection(connectionString);
        }

        // ── Save a sent notification ──────────────────────────────────────
        public void Save(Notification n, int userId)
        {
            string sql = @"INSERT INTO notifications 
                                (message, notification_type, sent_date, status, recipient_name, recipient_contact, user_id)
                           VALUES 
                                (@message, @type, @sentDate, @status, @recipientName, @recipientContact, @userId)
                           RETURNING id";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@message",          n.Message);
            command.Parameters.AddWithValue("@type",             n.NotificationType);
            command.Parameters.AddWithValue("@sentDate",         n.SentDate);
            command.Parameters.AddWithValue("@status",           n.Status);
            command.Parameters.AddWithValue("@recipientName",    n.RecipientName);
            command.Parameters.AddWithValue("@recipientContact", n.RecipientContact);
            command.Parameters.AddWithValue("@userId",           userId);

            try
            {
                connection.Open();
                object? result = command.ExecuteScalar();
                if (result != null)
                    n.Id = int.Parse(result.ToString()!);  
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public List<Notification> GetAll()
        {
            var notifications = new List<Notification>();

            string sql = @"SELECT 
                                n.id,
                                n.message,
                                n.notification_type,
                                n.sent_date,
                                n.status,
                                n.recipient_name,
                                n.recipient_contact,
                                u.name  AS user_name,
                                u.email AS user_email,
                                u.phone AS user_phone
                           FROM notifications n
                           INNER JOIN users u ON n.user_id = u.id
                           ORDER BY n.sent_date DESC";

            NpgsqlCommand command = new NpgsqlCommand(sql, connection);

            try
            {
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    notifications.Add(new Notification(
                        reader.GetInt32(0),     // n.id
                        reader.GetString(1),    // n.message
                        reader.GetString(2),    // n.notification_type
                        reader.GetDateTime(3),  // n.sent_date
                        reader.GetString(4),    // n.status
                        reader.GetString(5),    // n.recipient_name
                        reader.GetString(6)     // n.recipient_contact
                    ));
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }

            return notifications;  
        }
    }
}