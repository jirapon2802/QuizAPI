using QuizAPI.Models;

public class UserGroup
{
    public int UserGroupId { get; set; }
    public string GroupName { get; set; } = null!;
    public virtual ICollection<Quiz> Quizzes { get; set; }
    public virtual ICollection<User> Users { get; set; }
}

public class UserGroupViewModel
{
    public int UserGroupId { get; set; }
    public string GroupName { get; set; }
}
