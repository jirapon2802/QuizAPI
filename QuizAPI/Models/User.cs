namespace QuizAPI.Models;
public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public int UserGroupId { get; set; }
    public virtual ICollection<UserQuiz> UserQuiz { get; set; }
}

public class UserRequestViewModel
{
    public string Name { get; set; }
    public int UserGroupId { get; set; }
}