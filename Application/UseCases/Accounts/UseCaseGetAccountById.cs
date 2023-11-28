using Application.Dtos.Account;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.Repositories;

namespace Application.UseCases.Accounts;

public class UseCaseGetAccountById:IUseCaseParameterizedQuery<DtoOutputAccount, int>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public UseCaseGetAccountById(IAccountRepository userRepository, IMapper mapper)
    {
        _accountRepository = userRepository;
        _mapper = mapper;
    }

    public DtoOutputAccount Execute(int param)
    {
        var dbAccount = _accountRepository.FetchById(param);
        return _mapper.Map<DtoOutputAccount>(dbAccount);
    }
}