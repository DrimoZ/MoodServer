using System.Collections.Immutable;
using Domain;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework;

public class MoodContext: DbContext
{
    public MoodContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(user => user.IdUser);
            builder.Property(user => user.IdUser).HasColumnName("id_user");
            builder.Property(user => user.MailUser).HasColumnName("mail_user");
            builder.Property(user => user.LoginUser).HasColumnName("login_user");
            builder.Property(user => user.PasswordUser).HasColumnName("password_user");
            builder.Property(user => user.RoleUser).HasColumnName("role_user");
            builder.Property(user => user.TitreUser).HasColumnName("titre_user");
            builder.Property(user => user.IdAccount).HasColumnName("id_account");
        });
    }
}