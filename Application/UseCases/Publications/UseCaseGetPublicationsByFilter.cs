using Application.Dtos.Publication;
using Application.Dtos.User.UserData;
using Application.Services.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories.Communications;
using Infrastructure.EntityFramework.Repositories.Publications;
using Infrastructure.EntityFramework.Repositories.Users;

namespace Application.UseCases.Publications;

public class UseCaseGetPublicationsByFilter: IUseCaseParameterizedQuery<IEnumerable<DtoOutputDiscoverPublication>, string, int, string>
{
    private readonly IMapper _mapper;
    private readonly IPublicationRepository _publicationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPublicationService _publicationService;
    
    public UseCaseGetPublicationsByFilter(IMapper mapper, IUserRepository userRepository, IPublicationRepository publicationRepository, IPublicationService publicationService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _publicationRepository = publicationRepository;
        _publicationService = publicationService;
    }

    public IEnumerable<DtoOutputDiscoverPublication> Execute(string connectedUserId, int publicationCount, string searchValue)
    {
        var publications = _publicationService
            .FetchPublicationsWithoutUserId(connectedUserId, searchValue)
            .Select(p => _mapper.Map<DtoOutputDiscoverPublication>(p))
            .Reverse()
            .Take(publicationCount)
            .ToList();
        
        return publications;
    }
}