using Application.Dtos.Publication;
using Application.Services.Users;
using Application.Services.Users.Util;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationByFriend:IUseCaseParameterizedQuery<List<DtoOutputPublication>, string>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UseCaseGetPublicationByFriend(IPublicationRepository publicationRepository, IMapper mapper, IUserService userService)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
        _userService = userService;
    }

    public List<DtoOutputPublication> Execute(string userId)
    {
        var u = _userService.FetchById(userId, new[] { EUserFetchAttribute.Friends });
        var p = new List<DtoOutputPublication>();
        foreach (var friend in u.Friends())
        {
            p = _publicationRepository
                .FetchUserPublications(friend.Id)
                .Select(pub => _mapper.Map<DtoOutputPublication>(pub))
                .ToList();
        }
        return p;
    }
}