using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseCreatePublication: IUseCaseWriter<DbComplexPublication, DtoInputCreatePublication>
{
    private readonly IMapper _mapper;
    
    private readonly IPublicationRepository _publicationRepository;
    private readonly IPublicationElementRepository _publicationElementRepository;

    public UseCaseCreatePublication(IMapper mapper, IPublicationRepository publicationRepository, IPublicationElementRepository publicationElementRepository)
    {
        _mapper = mapper;
        
        _publicationRepository = publicationRepository;
        _publicationElementRepository = publicationElementRepository;
    }

    public DbComplexPublication Execute(DtoInputCreatePublication input)
    {
        var complexPub = _mapper.Map<DbComplexPublication>(input);

        var dbPub = _publicationRepository.Create(_mapper.Map<DbPublication>(complexPub));

        foreach (var element in complexPub.Elements)
        {
            element.IdPublication = dbPub.Id;
            _publicationElementRepository.Create(element);
        }

        return complexPub;
    }
}