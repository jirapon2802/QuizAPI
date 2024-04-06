using QuizAPI.Models;

namespace QuizAPI.Interface;
public interface IUserService 
{
    Task<int> CreateUser(UserRequestViewModel newUser);
    Task<UserGroupViewModel[]> GetUserGroup();
    Task<User> GetUserInfoByName(string name);
    Task<User> GetUserInfoById(int userId);
}