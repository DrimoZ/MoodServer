using Application.Dtos.Account;
using Application.UseCases.Utils;

namespace Application.UseCases.Account;

public class UseCaseGetAccountById:IUseCaseParameterizedQuery<DtoOutputAccount, int>
{
    public DtoOutputAccount Execute(int param)
    {
       
    }
}