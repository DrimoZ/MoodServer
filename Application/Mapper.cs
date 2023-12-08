using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.Dtos.Group;
using Application.Dtos.Message;
using Application.Dtos.Publication;
using Application.Dtos.User;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbComplexEntities;
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
        
        
        
        //UseCaseGetAllUsers
        CreateMap<DbUser, DtoOutputProfileUser>();
        //Users
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        CreateMap<DtoInputUpdateUser, DbUser>()
            .ForMember(dest => dest.Login, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        
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
        CreateMap<DbComplexPublication, DtoOutputPublication>();
        CreateMap<DtoInputCreatePublication, DbComplexPublication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore());
        //Message
        CreateMap<DtoInputMessage, DbMessage>();
        CreateMap<DbMessage, DtoOutputMessage>();
        
        //Friend
        CreateMap<DbFriend, DtoOutputFriend>();
    }
}