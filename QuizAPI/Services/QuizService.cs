using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QuizAPI.Interface;

namespace QuizAPI.Services;
public class QuizService : IQuizService
{
    private QuizDbContext _dbContext;
    public QuizService(QuizDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<QuizViewModel[]> GetQuizList(int userId)
    {
        var userInfo = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        var quiz = await _dbContext.Quizzes.Include(x => x.Answer).Where(x => x.UserGroupId == userInfo.UserGroupId).ToListAsync();
        var quizViewModel = MapToViewModel(quiz);
        return quizViewModel.ToArray();
    }

    public bool SaveUserQuiz(QuizViewModel[] saveQuiz, int userId)
    {
        var existingAnswers = _dbContext.UserQuizzes
            .Where(uq => uq.UserId == userId)
            .Select(uq => uq.AnswerId)
            .ToList();

        var selectedData = saveQuiz
            .Where(quiz => quiz.Option.Any(option => option.isSelected && !existingAnswers.Contains(option.AnswerId)))
            .Select(x => new UserQuiz
            {
                UserId = userId,
                QuizId = x.QuizId,
                AnswerId = x.Option.FirstOrDefault(option => option.isSelected).AnswerId
            });

        _dbContext.UserQuizzes.AddRange(selectedData);
        _dbContext.SaveChanges();
        return true;
    }

    public async Task<bool> SubmitUserQuiz(QuizViewModel[] saveQuiz, int userId)
    {
        // save data for answer 
         var existingAnswers = _dbContext.UserQuizzes
            .Where(uq => uq.UserId == userId)
            .Select(uq => uq.AnswerId)
            .ToList();

        var selectedData = saveQuiz
            .Where(quiz => quiz.Option.Any(option => option.isSelected && !existingAnswers.Contains(option.AnswerId)))
            .Select(x => new UserQuiz
            {
                UserId = userId,
                QuizId = x.QuizId,
                AnswerId = x.Option.FirstOrDefault(option => option.isSelected).AnswerId
            });

        _dbContext.UserQuizzes.AddRange(selectedData);

        // calculation for summary
        var answerIds = saveQuiz.SelectMany(x => x.Option).Where(option => option.isSelected == true).Select(d => d.AnswerId);
        var answerList = await _dbContext.Answers.Where(x => answerIds.Contains(x.AnswerId)).ToListAsync();
        var summaryData = saveQuiz.Select(x => new UserQuizSummary
        {
            UserId = userId,
            QuizId = x.QuizId,
            Point = answerList.First(answer => answer.AnswerId == x.Option.First(option => option.isSelected).AnswerId).Point
        });
        _dbContext.UserQuizSummaries.AddRange(summaryData);
        _dbContext.SaveChanges();
        return true;
    }

    public async Task<QuizViewModel[]> LoadUserQuiz(int userId)
    {
        var userInfo = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        var answerList = _dbContext.UserQuizzes.Where(x => x.UserId == userId);
        var quizList = await GetQuizList(userInfo.UserGroupId);

        foreach (var quiz in quizList)
        {
            var answerToBeFill = answerList.FirstOrDefault(x => x.QuizId == quiz.QuizId);
            if (answerToBeFill != null)
            {
                quiz.Option.First(x => x.AnswerId == answerToBeFill.AnswerId).isSelected = true;
            }
        }
        return quizList;
    }

    private static List<QuizViewModel> MapToViewModel(List<Quiz> quizzes)
    {
        var viewModels = new List<QuizViewModel>();
        foreach (var quiz in quizzes)
        {
            viewModels.Add(MapToSingleViewModel(quiz));
        }
        return viewModels;
    }

    private static QuizViewModel MapToSingleViewModel(Quiz quiz)
    {
        var viewModel = new QuizViewModel();
        viewModel.QuizId = quiz.QuizId;
        viewModel.QuestionQuize = quiz.QuestionQuize;
        viewModel.Option = quiz.Answer.Select(answer => new Option
        {
            AnswerId = answer.AnswerId,
            TextAnswerOption = answer.TextAnswer,
            isSelected = false
        }).ToList();
        return viewModel;
    }

    public async Task<UserQuizSummaryViewModel> GetUserQuizSummary(int userId)
    {
        var userInfo = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
        var summary = await _dbContext.UserQuizSummaries.Where(x => x.UserId == userId).ToListAsync();
        var quiz = await _dbContext.Quizzes.Include(x => x.Answer).Where(q => q.UserGroupId == userInfo.UserGroupId).ToListAsync();
        var summaryVm = new UserQuizSummaryViewModel
        {
            Name = userInfo.Name,
            Point = summary.Sum(x => x.Point),
            FullPoint = quiz.SelectMany(x => x.Answer).Sum(answer => answer.Point)
        };
        return summaryVm;
    }
}