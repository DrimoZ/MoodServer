using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseCreatePublication: IUseCaseWriter<DbComplexPublication, DtoInputCreatePublication>
{
    private IPublicationRepository _publicationRepository;
    private IMapper _mapper;

    public UseCaseCreatePublication(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public DbComplexPublication Execute(DtoInputCreatePublication input)
    {
        var dbAccount = _publicationRepository.Create(_mapper.Map<DbComplexPublication>(input));
        return dbAccount;
    }
}