using Notification_System_CRUD.Models;
using Notification_System_CRUD.Services;
using static Notification_System_CRUD.Presentation.InputHelper;
 
namespace Notification_System_CRUD.Presentation
{
    public static class NotificationMenu
    {
        public static void Show(UserService userService, NotificationService notificationService)
        {
            while(true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Notificaition Center");
                    Console.WriteLine();
                    Console.WriteLine("1. Send Notification");
                    Console.WriteLine("2. View Notifications");
                    Console.WriteLine("3. Back to Main Menu");
                    Console.WriteLine("Enter your choice: ");


                    int choice=Convert.ToInt32(Console.ReadLine());
                    if (choice ==3)
                        break;

                    switch (choice)
                    {
                        case 1:
                            {
                                var users=userService.GetUsers();
                                if (users.Count==0)
                                {
                                    Console.WriteLine("No users found! Please create users first.");
                                    break;
                                }

                                Console.WriteLine("Select a user to send notification:");
                                // foreach (var user in users)
                                // {
                                //     Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}" );
                                // }
                                //replace these lines with LINQ query

                                users.ForEach(u => Console.WriteLine($"[{u.Id}] {u.Name} | Email: {u.Email} | Phone: {u.PhoneNumber}"));

                                int userId=ReadPositiveInt("Enter User ID: ");

                                //LINQ: find selected user
                                User? selectedUser = users.FirstOrDefault(u=>u.Id==userId);

                                if  (selectedUser ==null)
                                    throw new UserNotFoundException(userId);

                                Console.WriteLine("Select notification type:");
                                Console.WriteLine("1. Email Notification");
                                Console.WriteLine("2. SMS Notification");
                                Console.WriteLine("Enter your choice: ");

                                int typeChoice=Convert.ToInt32(Console.ReadLine());

                                string notificationType=typeChoice switch
                                {
                                    1 => "Email",
                                    2 => "SMS",
                                    int t=> throw new InvalidNotificationTypeException(typeChoice.ToString())
                                    
                                };

                                Console.Write("\n Enter message: ");
                                string message=Console.ReadLine() ?? "";

                                notificationService.SendNotification(selectedUser,message,notificationType);
                                break;
                            }

                            case 2:
                            {
                                var allNotifications=notificationService.GetSentNotifications();
                                if (allNotifications.Count==0)
                                {
                                    Console.WriteLine("\n No Sent Notifications");
                                    break;
                                }

                                Console.WriteLine();
                                allNotifications.ForEach(n=>Console.WriteLine(
                                        $"Id: {n.Id}\n" +
                                        $"Notification Type: {n.NotificationType}\n" +
                                        $"Recipient Name: {n.RecipientName}\n" +
                                        $"Recipient Contact: {n.RecipientContact}\n" +
                                        $"Status: {n.Status}\n" +
                                        $"Sent Date: {n.SentDate:dd-MM-yyyy HH:mm:ss}"
                                    ));
                                break;
                            }

                            default:
                                Console.WriteLine("Invalid option.");
                                break;
                    }
                }

                catch(UserNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(InvalidNotificationTypeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(EmptyMessageException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (MessageTooShortException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (SmsTooLongException ex)
                {
                    Console.WriteLine(ex.Message);;
                }
                catch (InvalidEmailException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidPhoneException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
        }
    }
}