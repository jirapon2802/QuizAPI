namespace QuizAPI.Interface;

public interface IQuizService
{
    Task<QuizViewModel[]> GetQuizList(int userId);
    bool SaveUserQuiz(QuizViewModel[] saveQuiz, int userId);
    Task<bool> SubmitUserQuiz(QuizViewModel[] saveQuiz, int userId);
    Task<QuizViewModel[]> LoadUserQuiz(int userId);
    Task<UserQuizSummaryViewModel> GetUserQuizSummary(int userId);
}