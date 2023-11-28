using Application.Dtos.User;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users;

public class UseCaseGetUserByLoginAndMail : IUseCaseParameterizedQuery<DtoOutputUser, string, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UseCaseGetUserByLoginAndMail(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public DtoOutputUser Execute(string login, string mail)
    {
        var dbUser = _userRepository.FetchByLoginAndMail(login, mail);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}