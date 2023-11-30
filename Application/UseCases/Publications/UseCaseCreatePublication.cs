using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.UseCases.Utils;
using Infrastructure.EntityFramework.DbEntities;

namespace Application.UseCases.Publications;

public class UseCaseCreatePublication: IUseCaseWriter<DbPublication, DtoInputCreatePublication>
{
    public DbPublication Execute(DtoInputCreatePublication input)
    {
        throw new NotImplementedException();
    }
}