using ContactService.Data;
using Microsoft.EntityFrameworkCore;
using Messaging.Shared.Extensions;
using ContactService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IContactServices, ContactServices>();
builder.Services.AddHttpClient<IContactServices, ContactServices>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5036"); // Adjust to StudentService URL
});
builder.Services.AddMessagingServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
