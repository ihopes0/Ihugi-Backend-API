using Ihugi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ihugi.Infrastructure.ModelBuilders;

// TODO: XML docs
public class UserModelBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id).HasName("user_id");

        builder.Property(u => u.Name).HasColumnName("name_user");
        builder.Property(u => u.Password).HasColumnName("password");
        builder.Property(u => u.Email).HasColumnName("email");

        builder.HasMany(u => u.Chats)
            .WithMany(c => c.Users)
            .UsingEntity(j => j.ToTable("UserChat"));

        builder.HasMany(u => u.Messages)
            .WithOne()
            .HasForeignKey(m => m.AuthorId);

        builder.HasIndex(u => u.Email).IsUnique();
    }
}