using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users;

public class UseCaseGetAllUsers : IUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseGetAllUsers(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public void Execute()
    {
        
    }
}