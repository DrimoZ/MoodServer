using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Publications;

public class UseCaseCreatePublication: IUseCaseWriter<DbPublication, DtoInputCreatePublication>
{
    private IPublicationRepository _publicationRepository;
    private IMapper _mapper;

    public UseCaseCreatePublication(IPublicationRepository publicationRepository, IMapper mapper)
    {
        _publicationRepository = publicationRepository;
        _mapper = mapper;
    }

    public DbPublication Execute(DtoInputCreatePublication input)
    {
        var dbAccount = _publicationRepository.Create(_mapper.Map<DbPublication>(input));
        return dbAccount;
    }
}