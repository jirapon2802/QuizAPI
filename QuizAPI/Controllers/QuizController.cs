
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Interface;
using QuizAPI.Models;

namespace QuizAPI.Controllers;

[ApiController]
[EnableCors("default")]
public class QuizController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;
    public QuizController(IQuizService quizService,
                            IUserService userService)
    {
        _quizService = quizService;
        _userService = userService;
    }

    [HttpPost("api/register")]
    public async Task<IActionResult> RegisterUser([FromBody]UserRequestViewModel newUser)
    {
        var userId = await _userService.CreateUser(newUser);
        if(userId == 0) 
        {
            return BadRequest("There a problem with your request");
        }
        return new JsonResult(userId);
    }

    [HttpGet("api/userid/{name}")]
    public async Task<IActionResult> GetUserId(string name)
    {
        var userInfo = await _userService.GetUserInfoByName(name);
        if(userInfo == null)
        {
            return BadRequest("No user found");
        }
        return new JsonResult(userInfo.UserId);
    }

    [HttpGet("api/usergroup")]
    public async Task<IActionResult> GetUserGroup()
    {
        var userGroup = await _userService.GetUserGroup();
        return new JsonResult(userGroup);
    }

    [HttpGet("api/quiz/{userId}")]
    public async Task<IActionResult> GetQuizList(int userId)
    {
        var quizList = await _quizService.GetQuizList(userId);
        return new JsonResult(quizList);
    }
    
    [HttpGet("api/load/{userId}")]
    public async Task<IActionResult> LoadUserQuiz(int userId)
    {
        var userInfo = await _userService.GetUserInfoById(userId);
        if(userInfo == null) 
        {
            return BadRequest("There a problem with your request");
        } 
        var saveQuiz = await _quizService.LoadUserQuiz(userId);
        return new JsonResult(saveQuiz);
    }

    [HttpPost("api/save/{userId}")]
    public IActionResult SaveUserQuiz(int userId, [FromBody] QuizViewModel[] quizVm)
    {
        var success = _quizService.SaveUserQuiz(quizVm, userId);
        if (!success) 
        {
            return BadRequest("There a problem with your request");
        }
        return new JsonResult(success);
    }

    [HttpPost("api/submit/{userId}")]
    public async Task<IActionResult> SubmitUserQuiz(int userId, [FromBody] QuizViewModel[] quizVm)
    {
        var sumbit = await _quizService.SubmitUserQuiz(quizVm, userId);
        return new JsonResult(sumbit);
    }

    [HttpGet("api/summary/{userId}")]
    public async Task<IActionResult> GetUserSummary(int userId)
    {
        var summary = await _quizService.GetUserQuizSummary(userId);
        return new JsonResult(summary);
    }
}