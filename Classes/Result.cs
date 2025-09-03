namespace StepIT_ADO.NET_FinalProject.Classes;

internal class Result
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? CategoryId { get; set; }
    public int Score { get; set; }

    public virtual User User { get; set; }
    public virtual Category? Category { get; set; }
}
