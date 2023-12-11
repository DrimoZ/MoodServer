using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.Dtos.Group;
using Application.Dtos.Images;
using Application.Dtos.Message;
using Application.Dtos.Publication;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.Dtos.User.UserProfile;
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


        //User Profile
        CreateMap<DbUser, DtoOutputUserProfile>()
            .ForMember(dest => dest.FriendCount, opt => opt.Ignore())
            .ForMember(dest => dest.PublicationCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsConnectedUser, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore());
        
        //User Profile Account
        CreateMap<DbUser, DtoOutputUserAccount>()
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.Description, opt => opt.Ignore())
            .ForMember(dest => dest.BirthDate, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore());
        
        //User Profile Friends
        CreateMap<DbUser, DtoOutputUserFriends.DtoFriend>()
            .ForMember(dest => dest.CommonFriendCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsFriendWithConnected, opt => opt.Ignore());


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


        CreateMap<DtoInputCreatePublication, DbComplexPublication>();
        CreateMap<DbPublication, Publication>();

        CreateMap<DtoInputCreatePublication, DbPublication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore());

        CreateMap<DbComplexPublication, DbPublication>();

        CreateMap<DbPublication, DbComplexPublication>();

        CreateMap<DbComplexPublication, Publication>();


        CreateMap<DbPublicationElement, PublicationElement>();


        CreateMap<DbPublication, DtoOutputPublication>();
        CreateMap<Publication, DtoOutputPublication>();
        CreateMap<PublicationElement, DtoOutputPublication.DtoElements>();
        
        
        //User Profile Publications
        CreateMap<Publication, DtoOutputUserPublications.DtoPublication>();
        CreateMap<PublicationElement, DtoOutputUserPublications.DtoPublication.DtoElement>();

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
        //Friend
        CreateMap<DbFriend, DtoOutputFriend>();
        //Image
        CreateMap<DtoInputImage, DbImage>()
            .ForMember(dest => dest.Date, opt => opt.Ignore());
        CreateMap<DbImage, DtoOutputImage>();
    }
    
    
}