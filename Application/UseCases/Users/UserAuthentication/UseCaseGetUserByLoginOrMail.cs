using Application.Dtos.User.UserAuthentication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseGetUserByLoginOrMail : IUseCaseParameterizedQuery<DtoOutputUser, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UseCaseGetUserByLoginOrMail(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public DtoOutputUser Execute(string login)
    {
        var dbUser = _userRepository.FetchByLoginOrMail(login);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}