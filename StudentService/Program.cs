using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.Service;
using Messaging.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddMessagingServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
