using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseVerifySignUpUser : IUseCaseParameterizedQuery<bool, string, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UseCaseVerifySignUpUser(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public bool Execute(string connectedUserId, string profileRequestUserId)
    {
        _userRepository.FetchByLoginAndMail(connectedUserId, profileRequestUserId);
        return true;
    }
}