using Application.Dtos.User.UserData;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetUserInfoByLogin:IUseCaseParameterizedQuery<DtoOutputProfileUser, string, string>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IFriendRepository _friendRepository;
    
    public UseCaseGetUserInfoByLogin(IUserService userService, IMapper mapper, IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _userService = userService;
        _mapper = mapper;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }

    public DtoOutputProfileUser Execute(string userId, string otherLogin)
    {
        var user = _userRepository.FetchById(userId);
        var otherUser = _userRepository.FetchByLogin(otherLogin);
        try
        {
            var outputUser = _userService
                .FetchOtherById(otherUser.Id, new[] { EUserFetchAttribute.Data, EUserFetchAttribute.Account}, _friendRepository.IsFriend(user.Id, otherUser.Id));
            return _mapper.Map<DtoOutputProfileUser>(outputUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}