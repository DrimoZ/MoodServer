using Application.Dtos.Images;
using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Publications;

namespace Application.UseCases.Publications;

public class UseCaseCreatePublication: IUseCaseParameterizedWriter<DbComplexPublication, string, List<DtoInputImage>, string>
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationRepository _publicationRepository;
    private readonly IPublicationElementRepository _publicationElementRepository;
    private readonly IImageRepository _imageRepository;

    public UseCaseCreatePublication(IMapper mapper, IPublicationRepository publicationRepository, IPublicationElementRepository publicationElementRepository, IImageRepository imageRepository)
    {
        _mapper = mapper;
        
        _publicationRepository = publicationRepository;
        _publicationElementRepository = publicationElementRepository;
        _imageRepository = imageRepository;
    }

    public DbComplexPublication Execute(string connectedUserId, List<DtoInputImage> images, string content)
    {

        var complexPub = new DbComplexPublication
        {
            Content = content,
            Date = DateTime.Now,
            UserId = connectedUserId,
            Elements = new List<DbPublicationElement>()
        };
        
        var dbPub = _publicationRepository.Create(_mapper.Map<DbPublication>(complexPub));
        
        foreach (var dbElement in 
                 images.Select(image => _mapper.Map<DbImage>(image))
                     .Select(db => _imageRepository.Create(db))
                     .ToList()
                     .Select(dbImage => _publicationElementRepository
                         .Create(
                             new DbPublicationElement
                             {
                                 IdPublication = dbPub.Id, 
                                 IdImage = dbImage.Id
                             })
                     )
                 )
        {
            complexPub.Elements.Add(dbElement);
        }

        return complexPub;
    }
}