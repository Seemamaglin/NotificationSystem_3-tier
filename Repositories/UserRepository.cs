using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using NotificationSystem_3_tier.Contexts;

namespace NotificationSystem_3_tier.Repositories
{
    public class UserRepository : IUserRepository
    {
        NotificationContext context;

        public UserRepository()
        {
            context=new NotificationContext();
        }


        //ADO : NpgsqlCommand with INSERT SQL +connection.Open/Close
        //Ef Core : Add() marks state as added ->saveChanges() runs INSERT
        public void Add(User user)
        {
            context.users.Add(user);
            Console.WriteLine("State before SaveChanges: " + context.Entry<User>(user).State);
            context.SaveChanges();
            Console.WriteLine("State after SaveChanges: ");
            Console.WriteLine("User added to database successfully!");
        }

        //ADO : SELECT query +NpgsqlDataReader +manual object construction
        //EF core: ToList() ->EF core generates SELECT and maps rows to user object 

        public List<User> GetAll()
        {
            return context.users.ToList();
        }


        //ADO: 
        public User? GetById(int id)
        {
            return context.users.Find(id);
        }


        //ADO :  UPDATE SQL +parameters +ExecutionQuery
        //EF Core: update() marks state as modified and then SaveChanges() executes
        public void Update(User user)
        {
            User? existing = context.users.Find(user.Id);
            if (existing != null)
            {
                existing.Name = user.Name;
                existing.Email = user.Email;
                existing.PhoneNumber = user.PhoneNumber;

                Console.WriteLine("State before SaveChanges: " + context.Entry<User>(existing).State);
                context.SaveChanges();
                Console.WriteLine("State after SaveChanges: " + context.Entry<User>(existing).State);
                Console.WriteLine("User updated in database successfully!");
            }
            else
            {
                Console.WriteLine("No user found with that ID.");
            }
        }

        //Delete by userId
        //ADO : DELETE SQL + ExecuteNonQuery
        //EF Core : Find the object first, then .Remove() ->Save changes()

        public void Delete(int id)
        {
            User? user=context.users.Find(id);
            if(user!=null)
            {
                context.users.Remove(user);
                context.SaveChanges();
                Console.WriteLine("User deleted from database!");
            }
            else
            {
                Console.WriteLine("No user found with the ID.");
            }
        }
    }
} 