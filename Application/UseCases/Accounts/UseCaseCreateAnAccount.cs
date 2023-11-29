using Application.Dtos.Account;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Accounts;

public class UseCaseCreateAnAccount: IUseCaseWriter<DbAccount, DtoInputCreateAccount>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateAnAccount(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public DbAccount Execute(DtoInputCreateAccount inputCreate)
    {
        var dbAccount = _accountRepository.Create(_mapper.Map<DbAccount>(inputCreate));
        return dbAccount;
    }
}