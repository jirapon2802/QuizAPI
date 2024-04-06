public class Answer
{
    public int AnswerId { get; set; }
    public int QuizId { get; set; }
    public string TextAnswer { get; set; } = null!;
    public decimal Point { get; set; }
}