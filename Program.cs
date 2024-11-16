using System.Text.Json.Serialization;
using make_it_happen.Context;
using make_it_happen.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services.
builder.Services.AddControllers()
  .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database mySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Services.AddAutoMapper(typeof(Program));

// Configuração de autenticação JWT
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
  Console.WriteLine($"\n\n🚀 Servidor online: {app.Urls?.FirstOrDefault()} 🌐");
  Console.WriteLine($"🚀 Lembre de rodar o docker!\n\n");
});

// http://localhost:5135/swagger/index.html
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/ping", () =>
{
  return "pong!";
})
.WithName("Ping-Pong")
.WithOpenApi();

app.MapGet("/", () =>
{
  return "online!";
})
.WithName("Home")
.WithOpenApi();


app.MapControllers();
app.Run();
