public class UserQuizSummary
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public decimal Point { get; set; }
}

public class UserQuizSummaryViewModel
{
    public string Name { get; set; }
    public decimal Point { get; set; }
    public decimal FullPoint { get; set; }
}