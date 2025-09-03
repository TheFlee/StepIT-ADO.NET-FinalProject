namespace StepIT_ADO.NET_FinalProject.Classes;

internal class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public DateTime Birthdate { get; set; }

    public ICollection<Result> Results { get; set; }
}
