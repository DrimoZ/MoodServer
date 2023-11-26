using System.Text;
using Application.UseCases.Users;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;
using Mapper = Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Read Config Files
var configs = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.Development.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup Automapper
builder.Services.AddAutoMapper(typeof(Mapper));

// Setup Database
builder.Services.AddDbContext<MoodContext>(cfg => cfg.UseSqlServer(
    builder.Configuration.GetConnectionString("db")
));

//Database Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Use Cases
builder.Services.AddScoped<UseCaseGetUserByLoginOrMail>();

// Initialize JWT Bearer
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configs["JwtSettings:Issuer"],
            ValidAudience = configs["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configs["JwtSettings:SecretKey"]))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies[configs["JwtSettings:CookieName"]];

                if (string.IsNullOrEmpty(token)) return Task.CompletedTask;
                context.Token = token;

                return Task.CompletedTask;
            },
        };
    });

// Load TokenService Class
builder.Services.AddScoped<TokenService>();

// Initialize Loggers
builder.Services.AddLogging(b =>
{
    b.AddConsole();
    b.AddDebug();
});

// Initialize Dev Env
builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
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

//app.UseHttpsRedirection();

app.UseCors("Dev");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();