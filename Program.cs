using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using NotificationSystem_3_tier.Repositories;
using NotificationSystem_3_tier.Services;
using NotificationSystem_3_tier.Presentation;

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
                        UserMenu.Show(userService);
                        break;
                    case 2:
                        NotificationMenu.Show(userService,notificationService);
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
}