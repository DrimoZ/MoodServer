using Infrastructure.EntityFramework.DbComplexEntities;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class MoodContext: DbContext
{
    public MoodContext(DbContextOptions options) : base(options) { }
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbAccount> Accounts { get; set; }
    public DbSet<DbPublication> Publications { get; set; }
    public DbSet<DbPublicationElement> PublicationPhotos { get; set; }
    public DbSet<DbFriend> Friends { get; set; }
    public DbSet<DbGroup> Groups { get; set; }
    public DbSet<DbUserGroup> UserGroups { get; set; }
    public DbSet<DbCommunication> Communications { get; set; }
    public DbSet<DbMessage> Messages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbUser>(builder =>
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).HasColumnName("user_id");
            builder.Property(user => user.Mail).HasColumnName("user_mail");
            builder.Property(user => user.Login).HasColumnName("user_login");
            builder.Property(user => user.Name).HasColumnName("user_name");
            builder.Property(user => user.Password).HasColumnName("user_password");
            builder.Property(user => user.Role).HasColumnName("user_role");
            builder.Property(user => user.Title).HasColumnName("user_title");
            builder.Property(user => user.AccountId).HasColumnName("acc_id");
            builder.Property(user => user.IsDeleted).HasColumnName("user_isDeleted");
            builder.Property(user => user.IsPublic).HasColumnName("user_isPublic");
            builder.Property(user => user.IsFriendPublic).HasColumnName("user_isFriendPublic");
            builder.Property(user => user.IsPublicationPublic).HasColumnName("user_isPublicationPublic");
        });
        
        modelBuilder.Entity<DbAccount>(builder =>
        {
            builder.ToTable("accounts");
            builder.HasKey(account => account.Id);
            builder.Property(account => account.Id).HasColumnName("acc_id");
            builder.Property(account => account.PhoneNumber).HasColumnName("acc_phone_number");
            builder.Property(account => account.BirthDate).HasColumnName("acc_birth_date");
            builder.Property(account => account.Description).HasColumnName("acc_description");
        });
        
        modelBuilder.Entity<DbFriend>(builder =>
        {
            builder.ToTable("friends");
            builder.HasKey(friend => friend.UserId);
            builder.Property(friend => friend.UserId).HasColumnName("user_id");
            builder.Property(friend => friend.FriendId).HasColumnName("friend_id");
        });

        modelBuilder.Entity<DbPublication>(builder =>
        {
            builder.ToTable("publications");
            builder.HasKey(pub => pub.Id);
            builder.Property(pub => pub.Id).HasColumnName("pub_id");
            builder.Property(pub => pub.Content).HasColumnName("pub_content");
            builder.Property(pub => pub.UserId).HasColumnName("user_id");
            builder.Property(pub => pub.Date).HasColumnName("pub_date");
            builder.Property(pub => pub.IsDeleted).HasColumnName("pub_isDeleted");
        });

        modelBuilder.Entity<DbPublicationElement>(builder =>
        {
            builder.ToTable("photo_publications");
            builder.HasKey(pub => pub.Id);
            builder.Property(pub => pub.Id).HasColumnName("photoPub_id");
            builder.Property(pub => pub.Extension).HasColumnName("photoPub_extention");
            builder.Property(pub => pub.Content).HasColumnName("photoPub_content");
            builder.Property(pub => pub.IdPublication).HasColumnName("pub_id");
        });

        modelBuilder.Entity<DbGroup>(builder =>
        {
            builder.ToTable("groups");
            builder.HasKey(grp => grp.Id);
            builder.Property(grp => grp.Id).HasColumnName("group_id");
            builder.Property(grp => grp.IsDeleted).HasColumnName("group_isDeleted");
            builder.Property(grp => grp.Name).HasColumnName("group_name");
        });

        modelBuilder.Entity<DbUserGroup>(builder =>
        {
            builder.ToTable("user_groups");
            builder.HasKey(usrgrp => usrgrp.Id);
            builder.Property(usrgrp => usrgrp.Id).HasColumnName("user_group_id");
            builder.Property(usrgrp => usrgrp.UserId).HasColumnName("user_id");
            builder.Property(usrgrp => usrgrp.GroupId).HasColumnName("group_id");
        });
        modelBuilder.Entity<DbCommunication>(builder =>
        {
            builder.ToTable("communications");
            builder.HasKey(com => com.Id);
            builder.Property(com => com.Id).HasColumnName("comm_id");
            builder.Property(com => com.Date).HasColumnName("comm_date");
            builder.Property(com => com.IsDeleted).HasColumnName("comm_isDeleted");
        });
        
        modelBuilder.Entity<DbMessage>(builder =>
        {
            builder.ToTable("messages");
            builder.HasKey(msg => msg.Id);
            builder.Property(msg => msg.Id).HasColumnName("msg_id");
            builder.Property(msg => msg.Content).HasColumnName("msg_content");
            builder.Property(msg => msg.UserGroupId).HasColumnName("user_group_id");
            builder.Property(msg => msg.CommId).HasColumnName("comm_id");
        });
    }
}