using Application.Dtos.User.UserAuthentication;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users.UserAuthentication;

public class UseCaseGetUserByLogin
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UseCaseGetUserByLogin(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputUser Execute(string login)
    {
        try
        {
            var dbUser = _userRepository.FetchByLogin(login);
            return _mapper.Map<DtoOutputUser>(dbUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}