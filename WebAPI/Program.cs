using System.Text;
using Application.Dtos.Message;
using Application.Services.Publications;
using Application.Services.Users;
using Application.Services.Utils;
using Application.UseCases.Friends;
using Application.UseCases.Groups;
using Application.UseCases.Images;
using Application.UseCases.Messages;
using Application.UseCases.Publications;
using Application.UseCases.Users.User;
using Application.UseCases.Users.UserAuthentication;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories.Accounts;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Images;
using Infrastructure.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Hubs;
using Mapper = Application.AutoMapper.Mapper;

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
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IPublicationElementRepository, PublicationElementRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Application Services
builder.Services.AddScoped<IPublicationService, PublicationService>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddSingleton<IdService>();
builder.Services.AddSingleton<BCryptService>();

//Use Cases
builder.Services.AddScoped<UseCaseCreateUser>();
builder.Services.AddScoped<UseCaseVerifySignInUser>();
builder.Services.AddScoped<UseCaseVerifySignUpUser>();
builder.Services.AddScoped<UseCaseUpdateUserProfile>();
builder.Services.AddScoped<UseCaseFetchUserProfileByUserId>();
builder.Services.AddScoped<UseCaseUpdateUserProfilePicture>();
builder.Services.AddScoped<UseCasePatchUser>();
builder.Services.AddScoped<UseCaseFetchUserPrivacySettings>();
builder.Services.AddScoped<UseCaseDeleteUser>();
builder.Services.AddScoped<UseCaseUpdateUserPassword>();
builder.Services.AddScoped<UseCaseFetchUserNotifications>();


builder.Services.AddScoped<UseCaseFetchUserPublicationByUser>();
builder.Services.AddScoped<UseCaseGetPublicationById>();
builder.Services.AddScoped<UseCaseCreatePublication>();
builder.Services.AddScoped<UseCaseSetPublicationDeleted>();
builder.Services.AddScoped<UseCaseGetPublicationsByFilter>();
builder.Services.AddScoped<UseCaseLikePublication>();
builder.Services.AddScoped<UseCaseCommentPublication>();
builder.Services.AddScoped<UseCaseDeleteCommentInPublicationById>();
builder.Services.AddScoped<UseCaseGetCommentsByPublicationId>();
builder.Services.AddScoped<UseCaseGetFriendsPublications>();

builder.Services.AddScoped<UseCaseFetchUserAccountByUserId>();
builder.Services.AddScoped<UseCaseFetchUserFriendsByUserId>();
builder.Services.AddScoped<UseCaseFetchUsersByFilter>();


builder.Services.AddScoped<UseCaseGetFriendByUserId>();
builder.Services.AddScoped<UseCaseDeleteFriend>();
builder.Services.AddScoped<UseCaseCreateFriendRequest>();
builder.Services.AddScoped<UseCaseAcceptFriendRequest>();
builder.Services.AddScoped<UseCaseRejectFriendRequest>();
builder.Services.AddScoped<UseCaseDeleteMessageById>();
builder.Services.AddScoped<UseCaseSetMessageIsDeleted>();


builder.Services.AddScoped<UseCaseCreateGroup>();
builder.Services.AddScoped<UseCaseGetGroupsByUserId>();
builder.Services.AddScoped<UseCaseCreateMessage>();
builder.Services.AddScoped<UseCaseGetMessageFromGroup>();
builder.Services.AddScoped<UseCaseGetUserGroupByGroupIdUserId>();
builder.Services.AddScoped<UseCaseGetUsersFromGroup>();
builder.Services.AddScoped<UseCaseQuitGroup>();
builder.Services.AddScoped<UseCaseGetGroupById>();
builder.Services.AddScoped<UseCaseUpdateGroup>();
builder.Services.AddScoped<UseCaseUpdateGroupMembers>();

builder.Services.AddScoped<UseCaseGetImageById>();


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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configs["JwtSettings:SecretKey"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies[configs["JwtSettings:CookieName"]!];

                if (string.IsNullOrEmpty(token)) return Task.CompletedTask;
                context.Token = token;

                return Task.CompletedTask;
            },
        };
    });

// Initialize Loggers
builder.Services.AddLogging(b =>
{
    b.AddConsole();
    b.AddDebug();
});

//SignalR
builder.Services.AddSignalR();


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

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("Dev");
app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();
    
app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub>("/api/v1/message");
});


app.MapControllers();

app.Run();