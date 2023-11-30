using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.Services.Utils;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Application;

public class Mapper: Profile
{
    private readonly IdService _idService = new IdService();
    private readonly BCryptService _bCryptService = new BCryptService();
    
    public Mapper()
    {
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        CreateMap<DtoInputCreateUser, DbUser>()
            .BeforeMap((s, d) =>
            {
                s.Id = _idService.GenerateRandomId(32);
                d.Role = (int) UserRole.User;
                s.Password = _bCryptService.HashPassword(s.Password);
            });

        
        //Account
        CreateMap<DbAccount, DtoOutputAccount>();
        CreateMap<DbAccount, Account>();
        CreateMap<DbAccount, DtoInputCreateUser.DtoAccount>();
        
        CreateMap<DtoInputSignUpUser, DtoInputCreateAccount>();
        CreateMap<DtoInputCreateAccount, DbAccount>()
            .BeforeMap((s, d) => d.Id = _idService.GenerateRandomId(32) );

        
        
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();

    }
}