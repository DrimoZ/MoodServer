using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Users;

public class UseCaseGetAllUsers : IUseCase
{
    private readonly IUserRepository _todoRepository;
    private readonly IMapper _mapper;
    public void Execute()
    {
        
    }
}