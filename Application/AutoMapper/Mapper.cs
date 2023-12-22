using Application.Dtos.Friend;
using Application.Dtos.Group;
using Application.Dtos.Images;
using Application.Dtos.Message;
using Application.Dtos.Publication;
using Application.Dtos.User.User;
using Application.Dtos.User.UserAuthentication;
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
        CommentMappings();
        LikeMappings();
    }

    private void UserMappings()
    {

        // User Update
        CreateMap<DtoInputUserUpdateProfile, DbUser>();
        
        // User Sign In - Sign Up
        CreateMap<DtoInputUserSignUp, DtoInputUserCreate>();
        CreateMap<DbUser, DtoUserAuthenticate>();
        
        //Account Creation
        CreateMap<DtoInputUserSignUp, DtoInputUserCreate>();
        CreateMap<DtoInputUserCreate, DbUser>();

        //User Profile
        CreateMap<DbUser, DtoOutputUserProfile>();
        
        //User Profile Account
        CreateMap<DbUser, DtoOutputUserAccount>();
        
        //User Profile Friends
        CreateMap<DbUser, DtoOutputUserFriends.DtoFriend>();
        
        //Discover Users
        CreateMap<DbUser, DtoOutputDiscoverUser>();
        
        // Create Friend Request
        CreateMap<DbFriendRequest, DtoOutputFriendRequest>();
        
        //User Privacy Settings
        CreateMap<DbUser, DtoOutputUserPrivacy>();
        
        // User Notification
        CreateMap<DbFriendRequest, DtoOutputNotification>();
    }

    private void AccountMappings()
    {
        //Account Creation
        CreateMap<DtoInputUserSignUp, DbAccount>();
    }

    private void PublicationMappings()
    {
        CreateMap<DbPublication, Publication>();
        
        //DB Complex Publications
        CreateMap<DbPublication, DbComplexPublication>();
        
        //Publication Service
        CreateMap<DbComplexPublication, Publication>();
        CreateMap<DbPublicationElement, PublicationElement>();
        
        //User Profile Publications
        CreateMap<Publication, DtoOutputUserPublications.DtoPublication>();
        CreateMap<PublicationElement, DtoOutputUserPublications.DtoPublication.DtoPublicationElement>();
        
        // Discover Publications
        CreateMap<Publication, DtoOutputDiscoverPublication>();
        CreateMap<PublicationElement, DtoOutputDiscoverPublication.DtoElement>();

        // Publications Detail
        CreateMap<Publication, DtoOutputPublication>();
        CreateMap<PublicationElement, DtoOutputPublication.DtoOutputElement>();
        CreateMap<Comment, DtoOutputPublication.DtoOutputComment>();
        
        // Create Publication
        CreateMap<DbComplexPublication, DbPublication>();
    }

    private void GroupMappings()
    {
        CreateMap<DbGroup, DtoOutputGroup>();
        
        CreateMap<DtoInputCreateGroup, DbGroup>()
            .ForMember(dest => dest.Id, opt =>opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt =>opt.Ignore());
        CreateMap<DbUserGroup, DtoOutputUserGroup>();
        CreateMap<DtoInputUpdateGroup, DbGroup>()
            .ForMember(dest => dest.IsDeleted, opt =>opt.Ignore())
            .ForMember(dest => dest.IsPrivate, opt =>opt.Ignore());
        CreateMap<DtoInputUserGroup, DbUserGroup>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    private void MessageMappings()
    {
        CreateMap<DtoInputMessage, DbMessage>();
        CreateMap<DbMessage, DtoOutputMessage>();
        //Friend
        CreateMap<DbFriend, DtoOutputFriend>();
        //Image
        CreateMap<DtoInputImage, DbImage>()
            .ForMember(dest => dest.ImageDate, opt => opt.Ignore());
        CreateMap<DbImage, DtoOutputImage>();
    }

    private void CommentMappings()
    {
        // Publication Service
        CreateMap<DbComment, Comment>()
            .BeforeMap((s, d) => d.AuthorId = s.UserId);
        
        // Comment Publication
        CreateMap<DtoInputCommentPublication, DbComment>();
        
        // Get Comments From Publication
        CreateMap<DbComment, DtoOutputPublicationComment>()
            .BeforeMap((s, d) => d.AuthorId = s.UserId);
    }
    
    private void LikeMappings()
    {
        // Like Publication
        CreateMap<DtoInputLikePublication, DbLike>();
    }
}