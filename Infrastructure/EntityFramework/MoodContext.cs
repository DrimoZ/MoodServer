using System.Collections.Immutable;
using Domain;
using Infrastructure.EntityFramework.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class MoodContext: DbContext
{
    public MoodContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbAccount> Accounts { get; set; }
    
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
    }
}