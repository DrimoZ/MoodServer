using Application.Dtos.User;
using Application.Services.Users;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Users;

public class UseCaseGetUserByLoginOrMail : IUseCaseParameterizedQuery<DtoOutputUser, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    private readonly IUserService _userService;
    private readonly ILogger<UseCaseGetUserByLoginOrMail> _logger;
    
    public UseCaseGetUserByLoginOrMail(IUserRepository userRepository, IMapper mapper, IUserService userService, ILogger<UseCaseGetUserByLoginOrMail> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
    }
    
    public DtoOutputUser Execute(string login)
    {
        var dbUser = _userRepository.FetchByLoginOrMail(login);
        _logger.LogError(_userService.FetchById(dbUser.Id, new List<string> { "Account", "Friends" }).ToString());
        
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}