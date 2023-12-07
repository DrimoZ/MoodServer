using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetAllUsers: IUseCaseParameterizedQuery<List<DtoOutputProfileUser>, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseGetAllUsers(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public List<DtoOutputProfileUser> Execute(String userId)
    {
        return _userRepository
            .GetAll()
            .Where(user => user.Id != userId) 
            .Select(user => _mapper.Map<DtoOutputProfileUser>(user))
            .ToList();
    }
}