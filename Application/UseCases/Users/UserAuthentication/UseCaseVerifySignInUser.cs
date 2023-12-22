using Application.Dtos.User.UserAuthentication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseVerifySignInUser : IUseCaseParameterizedQuery<DtoUserAuthenticate, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UseCaseVerifySignInUser(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public DtoUserAuthenticate Execute(string login)
    {
        var dbUser = _userRepository.FetchByLoginOrMail(login);
        return _mapper.Map<DtoUserAuthenticate>(dbUser);
    }
}