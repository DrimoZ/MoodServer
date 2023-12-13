using Application.Dtos.User.UserAuthentication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseGetUserByLoginAndMail : IUseCaseParameterizedQuery<DtoOutputUser, string, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UseCaseGetUserByLoginAndMail(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public DtoOutputUser Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchByLoginAndMail(connectedUserId, profileRequestUserId);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}