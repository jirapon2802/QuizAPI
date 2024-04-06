using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QuizAPI.Interface;
using QuizAPI.Models;

namespace QuizAPI.Services;
public class UserService : IUserService
{
    private readonly QuizDbContext _dbContext;
    public UserService(QuizDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetUserInfoById(int userId)
    {
        var userInfo = await _dbContext.Users.FirstOrDefaultAsync(user => user.UserId == userId);
        return userInfo;
    }

    public async Task<UserGroupViewModel[]> GetUserGroup()
    {
        var userGroup = await _dbContext.UserGroups.ToListAsync();
        var ugVm = userGroup.Select(x => new UserGroupViewModel
        {
            UserGroupId = x.UserGroupId,
            GroupName = x.GroupName
        }).ToArray();
        return ugVm;
    }

    public async Task<int> CreateUser(UserRequestViewModel newUser)
    {
        var user = new User()
        {
            Name = newUser.Name,
            UserGroupId = newUser.UserGroupId
        };
        var foundUser = await _dbContext.Users.Where(x => x.Name == user.Name && x.UserGroupId == user.UserGroupId).ToArrayAsync();
        if(foundUser.Any())
        {
            throw new Exception("There a same user name in same user group");
        }
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        int userId = user.UserId;
        return userId; 
    }

    public async Task<User> GetUserInfoByName(string name)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Name == name);
        return user;
    }
}