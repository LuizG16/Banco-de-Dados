using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=CRUDAppDb;Trusted_Connection=True;");
    }
}
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
public void AddUser(User user)
{
    using (var context = new ApplicationDbContext())
    {
        context.Users.Add(user);
        context.SaveChanges();
    }
}
public List<User> GetUsers()
{
    using (var context = new ApplicationDbContext())
    {
        return context.Users.ToList();
    }
}
public void UpdateUser(User user)
{
    using (var context = new ApplicationDbContext())
    {
        context.Users.Update(user);
        context.SaveChanges();
    }
}
public void DeleteUser(int userId)
{
    using (var context = new ApplicationDbContext())
    {
        var user = context.Users.Find(userId);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        var userService = new UserService();

        userService.AddUser(new User { Name = "John Doe", Age = 30 });

        
        var users = userService.GetUsers();
        users.ForEach(user => Console.WriteLine($"{user.Id} - {user.Name} - {user.Age}"));

        var userToUpdate = users.FirstOrDefault();
        if (userToUpdate != null)
        {
            userToUpdate.Name = "Jane Doe";
            userService.UpdateUser(userToUpdate);
        }

        var userToDelete = users.LastOrDefault();
        if (userToDelete != null)
        {
            userService.DeleteUser(userToDelete.Id);
        }
    }
}

public class UserService
{
    public void AddUser(User user)
    {
        using (var context = new ApplicationDbContext())
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }

    public List<User> GetUsers()
    {
        using (var context = new ApplicationDbContext())
        {
            return context.Users.ToList();
        }
    }

    public void UpdateUser(User user)
    {
        using (var context = new ApplicationDbContext())
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }

    public void DeleteUser(int userId)
    {
        using (var context = new ApplicationDbContext())
        {
            var user = context.Users.Find(userId);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }
    }
}
