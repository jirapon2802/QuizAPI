using QuizAPI.Models;

public class UserQuiz
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int AnswerId { get; set; }
    public User Users { get; set; }
}