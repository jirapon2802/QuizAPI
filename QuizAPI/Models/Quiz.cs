public class Quiz
{
    public int QuizId { get; set; }
    public string QuestionQuize { get; set; } = null!;
    public int UserGroupId { get; set; }
    public virtual ICollection<Answer> Answer { get; set; }
}


public class QuizViewModel
{
    public int QuizId { get; set; }
    public string QuestionQuize { get; set; }
    public List<Option> Option { get; set; }
}

public class Option
{
    public int AnswerId { get; set; }
    public string TextAnswerOption { get; set; }
    public bool isSelected { get; set; }
}