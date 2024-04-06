using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

public class QuizDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<UserQuiz> UserQuizzes { get; set; }
    public DbSet<UserQuizSummary> UserQuizSummaries { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}