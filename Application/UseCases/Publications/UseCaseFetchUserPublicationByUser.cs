using Application.Dtos.Publication;
using Application.Dtos.User.UserProfile;
using Application.Services.Publications;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseFetchUserPublicationByUser:IUseCaseParameterizedQuery<DtoOutputUserPublications, string, string>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IPublicationService _publicationService;
    private readonly IFriendRepository _friendRepository;

    public UseCaseFetchUserPublicationByUser(IMapper mapper, IPublicationService publicationService, IUserRepository userRepository, IFriendRepository friendRepository)
    {
        _mapper = mapper;
        _publicationService = publicationService;
        _userRepository = userRepository;
        _friendRepository = friendRepository;
    }

    public DtoOutputUserPublications Execute(string connectedUserId, string profileRequestUserId)
    {
        var dbUser = _userRepository.FetchById(profileRequestUserId);
        var isSameUser = connectedUserId == profileRequestUserId;
        
        var dto = new DtoOutputUserPublications
        {
            IsConnectedUser = isSameUser,
            IsPublicationsPublic = isSameUser || _friendRepository.IsFriend(connectedUserId, profileRequestUserId) || dbUser is { IsPublic: true, IsPublicationPublic: true }
        };
        
        if (!dto.IsPublicationsPublic) return dto;

        var publications = _publicationService
                .FetchPublicationsByUserId(profileRequestUserId)
                .Select(p => _mapper.Map<DtoOutputUserPublications.DtoPublication>(p))
                .ToList();

        dto.Publications = publications;
        return dto;
    }
}