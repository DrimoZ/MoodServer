using Application.Dtos.Account;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Accounts;

public class UseCaseCreateAnAccount: IUseCaseWriter<DbAccount, DtoInputAccount>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateAnAccount(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    public DbAccount Execute(DtoInputAccount input)
    {
        var dbAccount = _accountRepository.Create(_mapper.Map<DbAccount>(input));
        return dbAccount;
    }
}