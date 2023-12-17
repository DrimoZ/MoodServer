using Application.Dtos.Account;
using Application.Dtos.Friend;
using Application.Dtos.Group;
using Application.Dtos.Images;
using Application.Dtos.Message;
using Application.Dtos.Publication;
using Application.Dtos.User.UserAuthentication;
using Application.Dtos.User.UserData;
using Application.Dtos.User.UserProfile;
using Application.Dtos.UserGroup;
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
        CreateMap<DbUser, DtoOutputUser>();
        
        CreateMap<DbUser, User>();
        
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        
        CreateMap<DtoInputUpdateUser, DbUser>()
            .ForMember(dest => dest.Login, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.AccountId, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());
        
        
        //Account Creation
        CreateMap<DtoInputSignUpUser, DbAccount>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<DtoInputSignUpUser, DtoInputCreateUser>();
        CreateMap<DtoInputCreateUser, DbUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        //User Profile
        CreateMap<DbUser, DtoOutputUserProfile>()
            .ForMember(dest => dest.IdImage, opt => opt.Ignore())
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
            .ForMember(dest => dest.IdImage, opt => opt.Ignore())
            .ForMember(dest => dest.CommonFriendCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsFriendWithConnected, opt => opt.Ignore());
    
        
        //Discover Users
        CreateMap<DbUser, DtoOutputUserDiscover>()
            .ForMember(dest => dest.IdImage, opt => opt.Ignore());

        
        // Create Friend Request
        CreateMap<DbFriendRequest, DtoOutputFriendRequest>();
        
        //User Privacy Settings
        CreateMap<DbUser, DtoOutputUserPrivacy>();
    }

    private void AccountMappings()
    {
        CreateMap<DbAccount, DtoOutputAccount>();
        
        CreateMap<DbAccount, Account>();
    }

    private void PublicationMappings()
    {
        CreateMap<DtoInputCreatePublication, DbComplexPublication>();
        CreateMap<DbPublication, Publication>();

        CreateMap<DtoInputCreatePublication, DbPublication>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Date, opt => opt.Ignore());
        
        //DB Complex Publications
        CreateMap<DbPublication, DbComplexPublication>();
        
        //Publication Service
        CreateMap<DbComplexPublication, Publication>();
        CreateMap<DbPublicationElement, PublicationElement>();
        
        //User Profile Publications
        CreateMap<Publication, DtoOutputUserPublications.DtoPublication>();
        CreateMap<PublicationElement, DtoOutputUserPublications.DtoPublication.DtoElement>();
        
        // Discover Publications
        CreateMap<Publication, DtoOutputDiscoverPublication>();
        CreateMap<PublicationElement, DtoOutputDiscoverPublication.DtoElement>();

        // Publications Detail
        CreateMap<Publication, DtoOutputPublication>();
        CreateMap<PublicationElement, DtoOutputPublication.DtoOutputElement>();
        CreateMap<Comment, DtoOutputPublication.DtoOutputComment>();
    }

    private void GroupMappings()
    {
        CreateMap<DbGroup, DtoOutputGroup>();
        
        CreateMap<DtoInputCreateGroup, DbGroup>()
            .ForMember(dest => dest.Id, opt =>opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt =>opt.Ignore());
        CreateMap<DbUserGroup, DtoOutputUserGroup>();
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