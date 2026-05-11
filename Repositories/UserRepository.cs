using NotificationSystem_3_tier.Models;
using NotificationSystem_3_tier.Interfaces;
using System.Net.Http.Headers;

namespace Notification_System_CRUD.Repositories
{
    public class UserRepository : IUserRepository
    {
        string connectionString = 
            "Host=localhost;Port=5433;Database=Notification_DB;Username=postgres;Password=Lovlin@2004";
        NpgsqlConnection connection;
    
        public UserRepository()
            {
                connection = new NpgsqlConnection(connectionString);   
            }


        //Add new user
        public void Add(User user)
        {
            string insertCmd="INSERT INTO users (id,name,email,phone) VALUES(@id,@name,@email,@phone)";
            NpgsqlCommand command=new NpgsqlCommand(insertCmd,connection);
            command.Parameters.AddWithValue("@id",user.Id);
            command.Parameters.AddWithValue("@name",user.Name);
            command.Parameters.AddWithValue("@email",user.Email);
            command.Parameters.AddWithValue("@phone",user.PhoneNumber);

            try
            {
                connection.Open();
                int result=command.ExecuteNonQuery();
                
                if (result>0)
                {
                    Console.WriteLine("User added to database successfully");
                }
            }
            catch (NpgsqlException ne)
            {
                throw new Exception("Database error while adding user: "+ne.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        //Get all users
        public List<User> GetAll()
        {
            var users=new List<User>();
            string get_users="SELECT id, name, email, phone FROM users";
            NpgsqlCommand command= new NpgsqlCommand(get_users,connection);

            try
            {
                connection.Open();
                NpgsqlDataReader reader=command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
            return users;
        }

        public User? GetById(int id)
        {
            User? user=null;
            string getUser="SELECT id,name,email,phone FROM users WHERE id=@id";
            NpgsqlCommand command = new NpgsqlCommand(getUser,connection);
            command.Parameters.AddWithValue("@id",id);

            try
            {
                connection.Open();
                NpgsqlDataReader reader=command.ExecuteReader();
                if (reader.Read())
                {
                    user=new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3)
                    );
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
            return user;
        }

        //Update existing user
        public void Update(User user)
        {
            string updateUser="UPDATE users SET name=@name,email=@email, phone=@phone WHERE id=@id";
            NpgsqlCommand command= new NpgsqlCommand(updateUser,connection);
            command.Parameters.AddWithValue("@name",user.Name);
            command.Parameters.AddWithValue("@email",user.Email);
            command.Parameters.AddWithValue("@phone",user.PhoneNumber);
            command.Parameters.AddWithValue("@id",user.Id);

            try
            {
                connection.Open();
                int result=command.ExecuteNonQuery();
                if (result>0)
                {
                    Console.WriteLine("User updated in database successfully.");
                }
                else
                    Console.WriteLine("No user found with this ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        //Delete user by ID
        public void Delete(int id)
        {
            string deleteUser="DELETE FROM users WHERE id=@id";
            NpgsqlCommand  command=new NpgsqlCommand(deleteUser,connection);
            command.Parameters.AddWithValue("@id",id);

            try
            {
                connection.Open();
                int result=command.ExecuteNonQuery();
                if (result>0)
                    Console.WriteLine("User deleted from database successfully.");
                else    
                    Console.WriteLine("No user found with that ID.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}