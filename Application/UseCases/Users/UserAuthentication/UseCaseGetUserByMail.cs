using Application.Dtos.User.UserAuthentication;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseGetUserByMail
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseGetUserByMail(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(string mail)
    {
        var dbUser = _userRepository.FetchByMail(mail);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}