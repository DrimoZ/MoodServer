using Application.Dtos.User.UserAuthentication;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseGetUserByName
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UseCaseGetUserByName(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(string name)
    {
        var dbUser = _userRepository.FetchByName(name);
        return _mapper.Map<DtoOutputUser>(dbUser);
    }
}