using Microsoft.EntityFrameworkCore;
using QuizAPI.Interface;
using QuizAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<QuizDbContext>();
builder.Services.AddTransient<IQuizService, QuizService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
