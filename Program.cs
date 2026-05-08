using Notification_System_CRUD.Models;
using Notification_System_CRUD.Interfaces;
using Notification_System_CRUD.Repositories;
using Notification_System_CRUD.Services;

class NotificationApp
{
    static void Main(string[] args)
    {
        var userRepository = new UserRepository();
        var notificationRepository = new NotificationRepository();
        var userService = new UserService(userRepository);
        var notificationService = new NotificationService(notificationRepository);

        while (true)
        {
            try
            {
                Console.WriteLine("================================ ");
                Console.WriteLine("   Simple Notification System");
                Console.WriteLine("================================ ");
                Console.WriteLine();
                Console.WriteLine("1. User Management");
                Console.WriteLine("2. Notification Management");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Select an option: ");
                int option= Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1: 
                        User_CRUD(userService);
                        break;
                    case 2:
                        NotificationCenter(userService,notificationService);
                        break;
                    case 3:
                        Console.WriteLine("Exiting the application!.");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Please select a valid option.");
                        break;
                }
            }    
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        } 
    }
   

    static void User_CRUD(UserService service)
    {
        while(true)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("User Management");
                Console.WriteLine();
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. View all users");
                Console.WriteLine("3. Update User");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("5. Back to Main Menu");
                Console.WriteLine();
                Console.Write(" Enter your choice: ");

                int choice=Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                if (choice == 5)
                    break;

                switch (choice)
                {
                    case 1:
                        {
                            int id=ReadPositiveInt("ID: ");

                            if (service.GetUsers().Exists(u=>u.Id==id))
                                throw new DuplicateUserException(id);

                            string name=ReadNonEmptyString("Name: ");
                            string email=ReadValidEmail("Email: ");
                            string phone=ReadValidPhone("Phone Number: ");

                            service.CreateUser(new User(id,name,email,phone));
                            Console.WriteLine("User created successfully!");
                            break;
                        }
                        
                    case 2:
                        {
                            var users=service.GetUsers();
                            if (users.Count==0)
                            {
                                Console.WriteLine("No users found");
                            }
                            //else
                            //{
                                //Console.WriteLine("Users:");
                                //foreach (var user in users)
                                //{
                                    //Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Phone: {user.PhoneNumber}");
                                //}
                            //}
                            //Replacing this ele part with LINQ
                            users.ForEach(u=>Console.WriteLine($"ID: {u.Id} | Name: {u.Name} | Email: {u.Email} | Phone: {u.PhoneNumber}"));
                            break;
                        }

                    case 3:
                        {
                            int updateId=ReadPositiveInt("Enter User ID: ");

                            if (!service.GetUsers().Exists(u=>u.Id==updateId))
                                throw new UserNotFoundException(updateId);

                            string newName=ReadNonEmptyString("New Name: ");
                            string newEmail=ReadValidEmail("New Email: ");
                            string newPhone=ReadValidPhone("New Phone Number: ");

                            service.UpdateUser(new User(updateId,newName,newEmail,newPhone));
                            Console.WriteLine("User updated successfully!");
                            break;
                        }

                    case 4:
                        {
                            int deleteId=ReadPositiveInt("Enter User Id: ");
                            if (!service.GetUsers().Exists(u=>u.Id==deleteId))
                                throw new UserNotFoundException(deleteId);

                            service.DeleteUser(deleteId);
                            Console.WriteLine("User deleted successfully!");
                            break;
                        }
                    default:
                        Console.WriteLine("Invalid choice! Please select a valid option.");
                        break;
                }
            }
            catch (DuplicateUserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
        
    static void NotificationCenter(UserService userService, NotificationService notificationService)
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

    static int ReadPositiveInt(string num)
    {
        while(true)
        {
            Console.Write(num);
            if (int.TryParse(Console.ReadLine(), out int value) && value > 0) 
                return value;
            Console.WriteLine("Please enter a positive number");
        }
    }

    static string ReadNonEmptyString(string msg)
    {
        while(true)
        {
            Console.Write(msg);
            string? message=Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(message))
                return message.Trim();
            Console.WriteLine("Message cannot be empty");
        }
    }

    static string ReadValidEmail(string mail)
    {
        while (true)
        {
            Console.Write(mail);
            string? email=Console.ReadLine()?.Trim();

            if(string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty");
                continue;
            }

            int atIndex=email.IndexOf('@');
            int dotIndex=email.LastIndexOf('.');
            
            if (atIndex >0  && dotIndex >atIndex+1 && dotIndex<email.Length-1)
            {
                return email;
            }
            Console.WriteLine($"'{email}' is not a valid email. Example: name@gamil.com");
            continue;
        }
    }

    static string ReadValidPhone(string number)
    {
        while(true)
        {
            Console.Write(number);
            string? num=Console.ReadLine()?.Trim();

            if(string.IsNullOrWhiteSpace(num))
            {
                Console.WriteLine("Phone number cannot be empty");
                continue;
            }

            int digitCount=0;
            bool allValid=true;

            foreach (char c in num)
            {
                if (char.IsDigit(c))
                {
                    digitCount++;
                    continue;
                }
                if (c=='+' || c=='-' || c==' ')
                    continue;
                allValid=false;
                break;
            }

            if (!allValid || digitCount<10)
                throw new InvalidPhoneException(num);

            return num;
        }
    }
}