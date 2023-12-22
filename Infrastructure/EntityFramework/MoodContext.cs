using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;
using DbFriendRequest = Infrastructure.EntityFramework.DbEntities.DbFriendRequest;

namespace Infrastructure.EntityFramework;

public class MoodContext: DbContext
{
    public MoodContext(DbContextOptions options) : base(options) { }
    
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbAccount> Accounts { get; set; }
    public DbSet<DbPublication> Publications { get; set; }
    public DbSet<DbPublicationElement> PublicationElements { get; set; }
    public DbSet<DbFriend> Friends { get; set; }
    public DbSet<DbGroup> Groups { get; set; }
    public DbSet<DbUserGroup> UserGroups { get; set; }
    public DbSet<DbMessage> Messages { get; set; }
    public DbSet<DbImage> Images { get; set; }
    public DbSet<DbLike> Likes { get; set; }
    public DbSet<DbComment> Comments { get; set; }
    public DbSet<DbFriendRequest> FriendRequests { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>(builder =>
        {
            builder.ToTable("users");
            builder.HasKey(user => user.UserId);
            builder.Property(user => user.UserId).HasColumnName("user_id");
            builder.Property(user => user.UserMail).HasColumnName("user_mail");
            builder.Property(user => user.UserLogin).HasColumnName("user_login");
            builder.Property(user => user.UserName).HasColumnName("user_name");
            builder.Property(user => user.UserPassword).HasColumnName("user_password");
            builder.Property(user => user.UserRole).HasColumnName("user_role");
            builder.Property(user => user.UserTitle).HasColumnName("user_title");
            builder.Property(user => user.AccountId).HasColumnName("acc_id");
            builder.Property(user => user.IsDeleted).HasColumnName("user_isDeleted");
            builder.Property(user => user.IsPublic).HasColumnName("user_isPublic");
            builder.Property(user => user.IsFriendPublic).HasColumnName("user_isFriendPublic");
            builder.Property(user => user.IsPublicationPublic).HasColumnName("user_isPublicationPublic");
        });
        
        modelBuilder.Entity<DbAccount>(builder =>
        {
            builder.ToTable("accounts");
            builder.HasKey(account => account.AccountId);
            builder.Property(account => account.AccountId).HasColumnName("acc_id");
            builder.Property(account => account.AccountPhoneNumber).HasColumnName("acc_phone_number");
            builder.Property(account => account.AccountBirthDate).HasColumnName("acc_birth_date");
            builder.Property(account => account.AccountDescription).HasColumnName("acc_description");
            builder.Property(account => account.ImageId).HasColumnName("img_id");
        });
        
        modelBuilder.Entity<DbFriend>(builder =>
        {
            builder.ToTable("friends");
            builder.HasKey(f =>new {f.UserId, f.FriendId});
            builder.Property(friend => friend.UserId).HasColumnName("user_id");
            builder.Property(friend => friend.FriendId).HasColumnName("friend_id");
        });

        modelBuilder.Entity<DbPublication>(builder =>
        {
            builder.ToTable("publications");
            builder.HasKey(pub => pub.PublicationId);
            builder.Property(pub => pub.PublicationId).HasColumnName("pub_id");
            builder.Property(pub => pub.PublicationContent).HasColumnName("pub_content");
            builder.Property(pub => pub.UserId).HasColumnName("user_id");
            builder.Property(pub => pub.PublicationDate).HasColumnName("pub_date");
            builder.Property(pub => pub.IsDeleted).HasColumnName("pub_isDeleted");
        });

        modelBuilder.Entity<DbPublicationElement>(builder =>
        {
            builder.ToTable("publication_elements");
            builder.HasKey(elem => elem.ElementId);
            builder.Property(elem => elem.ElementId).HasColumnName("elmt_id");
            builder.Property(elem => elem.ImageId).HasColumnName("img_id");
            builder.Property(elem => elem.PublicationId).HasColumnName("pub_id");
        });

        modelBuilder.Entity<DbGroup>(builder =>
        {
            builder.ToTable("groups");
            builder.HasKey(grp => grp.Id);
            builder.Property(grp => grp.Id).HasColumnName("group_id");
            builder.Property(grp => grp.IsDeleted).HasColumnName("group_isDeleted");
            builder.Property(grp => grp.Name).HasColumnName("group_name");
            builder.Property(grp => grp.IsPrivate).HasColumnName("group_isPrivate");
            builder.Property(grp => grp.ProprioId).HasColumnName("group_proprio_id");
        });

        modelBuilder.Entity<DbUserGroup>(builder =>
        {
            builder.ToTable("user_groups");
            builder.HasKey(usrgrp => usrgrp.Id);
            builder.Property(usrgrp => usrgrp.Id).HasColumnName("user_group_id");
            builder.Property(usrgrp => usrgrp.UserId).HasColumnName("user_id");
            builder.Property(usrgrp => usrgrp.GroupId).HasColumnName("group_id");
            builder.Property(usrgrp => usrgrp.HasLeft).HasColumnName("user_has_left");
        });
        
        modelBuilder.Entity<DbMessage>(builder =>
        {
            builder.ToTable("messages");
            builder.HasKey(msg => msg.Id);
            builder.Property(msg => msg.Id).HasColumnName("msg_id");
            builder.Property(msg => msg.Content).HasColumnName("msg_content");
            builder.Property(msg => msg.UserGroupId).HasColumnName("user_group_id");
            builder.Property(msg => msg.Date).HasColumnName("msg_date");
            builder.Property(msg => msg.IsDeleted).HasColumnName("msg_isDeleted");
        });

        modelBuilder.Entity<DbImage>(builder =>
        {
            builder.ToTable("images");
            builder.HasKey(img => img.ImageId);
            builder.Property(img => img.ImageId).HasColumnName(("img_id"));
            builder.Property(img => img.ImageData).HasColumnName("img_data");
            builder.Property(img => img.ImageDate).HasColumnName("img_date");
        });
        
        
        modelBuilder.Entity<DbLike>(builder =>
        {
            builder.ToTable("likes");
            builder.HasKey(like => like.LikeId);
            builder.Property(like => like.LikeId).HasColumnName("like_id");
            builder.Property(like => like.LikeDate).HasColumnName("like_date");
            builder.Property(like => like.PublicationId).HasColumnName("pub_id");
            builder.Property(like => like.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<DbComment>(builder =>
        {
            builder.ToTable("comments");
            builder.HasKey(cmt => cmt.CommentId);
            builder.Property(cmt => cmt.CommentId).HasColumnName("cmt_id");
            builder.Property(cmt => cmt.CommentDate).HasColumnName("cmt_date");
            builder.Property(cmt => cmt.CommentContent).HasColumnName("cmt_content");
            builder.Property(cmt => cmt.PublicationId).HasColumnName("pub_id");
            builder.Property(cmt => cmt.UserId).HasColumnName("user_id");
        });
        
        modelBuilder.Entity<DbFriendRequest>(builder =>
        {
            builder.ToTable("friend_requests");
            builder.HasKey(fr => fr.FriendRequestId);
            builder.Property(fr => fr.FriendRequestId).HasColumnName("req_id");
            builder.Property(fr => fr.FriendRequestDate).HasColumnName("req_date");
            builder.Property(fr => fr.IsDone).HasColumnName("req_isDone");
            builder.Property(fr => fr.IsAccepted).HasColumnName("req_isAccepted");
            builder.Property(fr => fr.UserId).HasColumnName("user_id");
            builder.Property(fr => fr.FriendId).HasColumnName("friend_id");
        });
    }
}