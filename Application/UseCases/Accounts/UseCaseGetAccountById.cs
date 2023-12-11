using Application.Dtos.Account;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;
using Infrastructure.EntityFramework.Repositories.Accounts;

namespace Application.UseCases.Accounts;

public class UseCaseGetAccountById:IUseCaseParameterizedQuery<DtoOutputAccount, string>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseGetAccountById(IAccountRepository userRepository, IMapper mapper)
    {
        _accountRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputAccount Execute(string param)
    {
        var dbAccount = _accountRepository.FetchById(param);
        return _mapper.Map<DtoOutputAccount>(dbAccount);
    }
}