using Notification_System_CRUD.Models;
using Notification_System_CRUD.Services;
using static Notification_System_CRUD.Presentation.InputHelper;
 
namespace Notification_System_CRUD.Presentation
{
    public static class UserMenu
    {
        public static void Show(UserService service)
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
    }
}