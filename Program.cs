using System.Text.Json.Serialization;
using make_it_happen.Context;
using make_it_happen.Models;
using make_it_happen.Repositories;
using make_it_happen.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
var secretKey = builder.Configuration["Jwt:SecretKey"]
?? throw new ArgumentException("A chave secreta 'Jwt:SecretKey' deve ser configurada no appsettings.json.");
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = TimeSpan.Zero,
    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(
      System.Text.Encoding.UTF8.GetBytes(secretKey)),
  };
});
builder.Services.AddScoped<ITokenService, TokenService>();

// Identity Framework
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

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
