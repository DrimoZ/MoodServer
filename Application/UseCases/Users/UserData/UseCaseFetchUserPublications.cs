using Application.Dtos.User.UserData;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;

namespace Application.UseCases.Users.UserData;

public class UseCaseFetchUserPublications: IUseCaseParameterizedQuery<DtoOutputProfileUser, string>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UseCaseFetchUserPublications(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public DtoOutputProfileUser Execute(string userId)
    {
        var user = _userService.FetchById(userId, new[] { EUserFetchAttribute.Data, EUserFetchAttribute.Account, EUserFetchAttribute.Publications});
        return _mapper.Map<DtoOutputProfileUser>(user);
    }
}