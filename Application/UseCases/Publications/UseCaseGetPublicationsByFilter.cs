using Application.Dtos.Publication;
using Application.Dtos.User.UserData;
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

    private static Random rng = new Random();
    
    public UseCaseGetPublicationsByFilter(IMapper mapper, IUserRepository userRepository, IPublicationRepository publicationRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _publicationRepository = publicationRepository;
    }

    public IEnumerable<DtoOutputDiscoverPublication> Execute(string connectedUserId, int publicationCount, string searchValue)
    {
        var publications = _publicationRepository
            .FetchPublicationsByFilter(connectedUserId)
            .Where(publication => _userRepository.FetchById(publication.UserId).Name.ToLower().Contains(searchValue)) 
            .Select(publication => _mapper.Map<DtoOutputDiscoverPublication>(publication))
            .ToList();
        
        return publications.OrderBy(a => rng.Next()).Take(publicationCount).ToList();
    }
}