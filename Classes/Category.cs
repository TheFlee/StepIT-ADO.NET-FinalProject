namespace StepIT_ADO.NET_FinalProject.Classes;

internal class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Result> Results { get; set; }
}
