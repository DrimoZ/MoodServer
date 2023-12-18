using Application.Dtos.User.UserData;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Users.User;

public class UseCaseGetUserPrivacySettings: IUseCaseParameterizedQuery<DtoOutputUserPrivacy, string>
{
    private readonly IMapper _mapper;
    
    private readonly IUserRepository _userRepository;

    public UseCaseGetUserPrivacySettings(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        
        _userRepository = userRepository;
    }


    public DtoOutputUserPrivacy Execute(string connectedUserId)
    {
        return _mapper.Map<DtoOutputUserPrivacy>(_userRepository.FetchById(connectedUserId));
    }
}