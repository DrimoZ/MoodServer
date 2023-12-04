using Application.Dtos.Account;
using Application.Dtos.Group;
using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
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
        
        
        //UseCaseFetchUserAccount //UseCaseFetchUserFriends
        CreateMap<User, DtoOutputProfileUser>();
        //UseCaseFetchUserAccount
        CreateMap<Account, DtoOutputProfileUser.DtoOutputAccount>();
        //UseCaseFetchUserPublications
        CreateMap<Publication, DtoOutputProfileUser.DtoOutputPublication>();
        
        
        
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();

        
        //Account
        CreateMap<DbAccount, DtoOutputAccount>();
        CreateMap<DbAccount, Account>();
        CreateMap<DbAccount, DtoInputCreateUser.DtoAccount>();
        
        //Group
        CreateMap<DbGroup, DtoOutputGroup>();
        CreateMap<DtoInputCreateGroup, DbGroup>()
            .ForMember(dest => dest.Id, opt =>opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt =>opt.Ignore());
        
        //Publication
        CreateMap<DbPublication, DtoOutputPublication>();
        CreateMap<DtoInputCreatePublication, DbPublication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore());

    }
}