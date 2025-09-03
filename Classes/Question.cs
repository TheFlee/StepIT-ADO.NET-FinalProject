namespace StepIT_ADO.NET_FinalProject.Classes;

internal class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int CategoryId { get; set; }

    public ICollection<Answer> Answers { get; set; }

    public virtual Category Category { get; set; }
}
