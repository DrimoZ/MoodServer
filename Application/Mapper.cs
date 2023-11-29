using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.Services.Utils;
using AutoMapper;
using Infrastructure.EntityFramework.DbEntities;

namespace Application;

public class Mapper: Profile
{
    private readonly IdService _idService = new IdService();
    public Mapper()
    {
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        CreateMap<DtoInputCreateUser, DbUser>()
            .BeforeMap((s, d) => s.Id = _idService.GenerateRandomId(32) )
            .BeforeMap((s, d) => d.Role = 1 );

        
        //Account
        CreateMap<DtoInputSignUpUser, DtoInputCreateAccount>();
        CreateMap<DbAccount, DtoOutputAccount>();
        CreateMap<DtoInputCreateAccount, DbAccount>()
            .BeforeMap((s, d) => d.Id = _idService.GenerateRandomId(32) );

        CreateMap<DbAccount, DtoInputCreateUser.DtoAccount>();
        
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();

    }
}