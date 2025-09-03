using StepIT_ADO.NET_FinalProject.Classes;
using StepIT_ADO.NET_FinalProjectl;

namespace StepIT_ADO.NET_FinalProject.Service;

internal class UserService
{
    private readonly QuizContext _context;

    public UserService(QuizContext context)
    {
        _context = context;
    }

    public bool Register(string username, string password, DateTime birthDate)
    {
        if (_context.Users.Any(u => u.Username == username))
            return false;

        var user = new User
        {
            Username = username,
            Password = password,
            Birthdate = birthDate
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return true;
    }
    public User? Login(string username, string password)
    {
        return _context.Users
            .FirstOrDefault(u => u.Username == username && u.Password == password);
    }
    public bool UpdatePassword(int userId, string newPassword)
    {
        var user = _context.Users.Find(userId);
        if (user == null) return false;

        user.Password = newPassword;
        _context.SaveChanges();
        return true;
    }
    public bool UpdateBirthDate(int userId, DateTime birthDate)
    {
        var user = _context.Users.Find(userId);
        if (user == null) return false;

        user.Birthdate = birthDate;
        _context.SaveChanges();
        return true;
    }
}
