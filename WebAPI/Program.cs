using System.Text;
using Application.Services.Users;
using Application.Services.Utils;
using Application.UseCases.Accounts;
using Application.UseCases.Friends;
using Application.UseCases.Groups;
using Application.UseCases.Messages;
using Application.UseCases.Publications;
using Application.UseCases.Users;
using Application.UseCases.Users.UserAuthentication;
using Application.UseCases.Users.UserData;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
).EnableDetailedErrors());

//Database Repositories & Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPublicationRepository, PublicationRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();
builder.Services.AddScoped<ICommunicationRepository, CommunicationRepository>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<IUserService, UserService>();

//Use Cases
builder.Services.AddScoped<UseCaseCreateUser>();
builder.Services.AddScoped<UseCaseGetUserByLoginOrMail>();
builder.Services.AddScoped<UseCaseGetUserByLoginAndMail>();
builder.Services.AddScoped<UseCaseGetUserByLogin>();
builder.Services.AddScoped<UseCaseGetUserByMail>();
builder.Services.AddScoped<UseCaseUpdateUserData>();

builder.Services.AddScoped<UseCaseGetAccountById>();
builder.Services.AddScoped<UseCaseCreateAnAccountTODEL>();

builder.Services.AddScoped<UseCaseGetPublicationByUser>();
builder.Services.AddScoped<UseCaseGetPublicationByFriend>();
builder.Services.AddScoped<UseCaseGetPublicationById>();
builder.Services.AddScoped<UseCaseCreatePublication>();
builder.Services.AddScoped<UseCaseDeletePublication>();
builder.Services.AddScoped<UseCaseSetPublicationDeleted>();

builder.Services.AddScoped<UseCaseFetchUserAccount>();
builder.Services.AddScoped<UseCaseFetchUserPublications>();
builder.Services.AddScoped<UseCaseFetchUserFriends>();
builder.Services.AddScoped<UseCaseGetUserInfoByLogin>();
builder.Services.AddScoped<UseCaseGetAllUsers>();

builder.Services.AddScoped<UseCaseCreateFriend>();
builder.Services.AddScoped<UseCaseGetFriendByUserId>();
builder.Services.AddScoped<UseCaseDeleteFriend>();

builder.Services.AddScoped<UseCaseCreateGroup>();
builder.Services.AddScoped<UseCaseCreateMessage>();

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

// Load Services Class
builder.Services.AddScoped<TokenService>();
builder.Services.AddSingleton<IdService>();
builder.Services.AddSingleton<BCryptService>();

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
            .AllowCredentials();
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