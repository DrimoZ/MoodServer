using Application.Dtos.Account;
using Application.Dtos.Publication;
using Application.Dtos.User;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbEntities;

namespace Application;

public class Mapper: Profile
{
    public Mapper()
    {
        //UseCaseCreateUser
        CreateMap<DtoInputSignUpUser, DbAccount>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        //UseCaseCreateUser
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        //UseCaseCreateUser
        CreateMap<DtoInputCreateUser, DbUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        
        
        
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();

        
        //Account
        CreateMap<DbAccount, DtoOutputAccount>();
        CreateMap<DbAccount, Account>();
        CreateMap<DbAccount, DtoInputCreateUser.DtoAccount>();
        
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();
        CreateMap<DtoInputCreatePublication, DbPublication>();
        /*.BeforeMap((s, d) =>
        {
            d.Id = _idService.GenerateRandomId(32);
            d.Date = DateTime.Now;
        });*/

    }
}