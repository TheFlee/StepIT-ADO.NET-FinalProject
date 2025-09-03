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
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return false;

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
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return null;
            
        return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }
    public void UpdateUser(int userId, string? password, DateTime? birthDate)
    {
        var user = _context.Users.Find(userId);
        if (user == null) return;

        if (!string.IsNullOrEmpty(password))
            user.Password = password;
        if (birthDate is not null)
            user.Birthdate = birthDate.Value;

        _context.SaveChanges();
    }
}
