using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.UserData;

public class UseCaseGetUserPrivacySettings: IUseCaseParameterizedQuery<DtoOutputUserPrivacy, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseGetUserPrivacySettings(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public DtoOutputUserPrivacy Execute(string connectedUserId)
    {
        return _mapper.Map<DtoOutputUserPrivacy>(_userRepository.FetchById(connectedUserId));
    }
}