using Application.Dtos.Account;
using Application.Dtos.Group;
using Application.Dtos.Message;
using Application.Dtos.Publication;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using AutoMapper;
using Domain;
using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;

namespace Application.AutoMapper;

public class Mapper: Profile
{
    public Mapper()
    {
        UserMappings();
        AccountMappings();
        PublicationMappings();
        GroupMappings();
        MessageMappings();
    }

    private void UserMappings()
    {
        CreateMap<DtoInputSignUpUser, DbAccount>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();

        CreateMap<DtoInputCreateUser, DbUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        CreateMap<User, DtoOutputProfileUser>();
        
        CreateMap<DbUser, DtoOutputProfileUser>();

        CreateMap<DbUser, DtoOutputUser>();
        
        CreateMap<DbUser, User>();
        
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        
        CreateMap<DtoInputUpdateUser, DbUser>()
            .ForMember(dest => dest.Login, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
    }

    private void AccountMappings()
    {
        CreateMap<Account, DtoOutputProfileUser.DtoOutputAccount>();
        
        CreateMap<DbAccount, DtoOutputAccount>();
        
        CreateMap<DbAccount, Account>();
        
        CreateMap<DbAccount, DtoInputCreateUser.DtoAccount>();
        
    }

    private void PublicationMappings()
    {
        CreateMap<Publication, DtoOutputProfileUser.DtoOutputPublication>();

        CreateMap<Publication, DtoOutputPublication>();
        
        CreateMap<DtoInputCreatePublication, DbComplexPublication>()
        CreateMap<DbPublication, DtoOutputPublication>();
        CreateMap<DbPublication, Publication>();
            
        CreateMap<DtoInputCreatePublication, DbPublication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore());
        
        CreateMap<DbComplexPublication, DbPublication>();
        
        CreateMap<DbPublication, DbComplexPublication>();
        
        CreateMap<DbComplexPublication, Publication>();
    }

    private void GroupMappings()
    {
        CreateMap<DbGroup, DtoOutputGroup>();
        
        CreateMap<DtoInputCreateGroup, DbGroup>()
            .ForMember(dest => dest.Id, opt =>opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt =>opt.Ignore());
    }

    private void MessageMappings()
    {
        CreateMap<DtoInputMessage, DbMessage>();
        CreateMap<DbMessage, DtoOutputMessage>();
    }
}