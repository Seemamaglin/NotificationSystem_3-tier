namespace Notification_System_CRUD.Models;
public class User
{
    public int Id {get; set;}
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public User(int id, string name, string email, string phoneNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}