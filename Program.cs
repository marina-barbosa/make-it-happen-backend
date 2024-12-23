using System.Text.Json.Serialization;
using make_it_happen.Context;
using make_it_happen.Models;
using make_it_happen.Repositories;
using make_it_happen.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services.
builder.Services.AddControllers()
  .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// CORS
var OrigensPermitidas = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
options.AddPolicy(name: OrigensPermitidas,
  policy =>
  {
    policy.WithOrigins("http://localhost:4200")
      .AllowAnyMethod()
      .AllowAnyHeader();
  })
);
// builder.Services.AddCors(options =>
// {
//   options.AddPolicy("AllowOrigin", builder => builder
//     .AllowAnyOrigin()
//     .AllowAnyMethod()
//     .AllowAnyHeader());
// });

builder.Services.AddEndpointsApiExplorer();
// SWAGGER
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new() { Title = "make_it_happen", Version = "v1" });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
  {
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Bearer JWT."
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        }
      },
      new string[] { }
    }
  });
});

// Database mySQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddAutoMapper(typeof(Program));

// Configuração de autenticação JWT
var secretKey = builder.Configuration["Jwt:SecretKey"]
?? throw new ArgumentException("A chave secreta 'Jwt:SecretKey' deve ser configurada no appsettings.json.");
builder.Services.AddAuthentication("Bearer").AddJwtBearer(
                  options =>
                    {
                      options.SaveToken = true;
                      options.RequireHttpsMetadata = false;
                      // options.Authority = "http://localhost:5135";
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
                    }
);
builder.Services.AddAuthorization(options =>
{
  options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
  options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
  options.AddPolicy("AdminOrUser", policy => policy.RequireRole("Admin", "User"));
  options.AddPolicy("SuperAdminOnly", policy => policy.RequireRole("SuperAdmin")
    .RequireClaim("id", "hortencia"));
  options.AddPolicy("ExclusiveOnly", policy =>
  {
    policy.RequireAssertion(context =>
    context.User.HasClaim(claim => claim.Type == "id" && claim.Value == "hortencia")
                          || context.User.IsInRole("SuperAdmin"));
  });
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

// forçar o redirecionamento de HTTP para HTTPS
// app.UseHttpsRedirection();


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

app.UseCors(OrigensPermitidas);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
