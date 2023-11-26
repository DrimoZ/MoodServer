using Application.Dtos.User;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users;

public class UseCaseGetUserByLoginOrMail : IUseCase
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