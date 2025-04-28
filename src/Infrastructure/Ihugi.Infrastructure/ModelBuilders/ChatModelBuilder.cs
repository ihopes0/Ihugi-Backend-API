using Ihugi.Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ihugi.Infrastructure.ModelBuilders;

// TODO: XML docs
public class ChatModelBuilder : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id).HasName("chat_id");
        
        builder.Property(c => c.Name).HasColumnName("name_chat");

        builder.HasMany(c => c.Messages)
            .WithOne()
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Users)
            .WithMany(u => u.Chats)
            .UsingEntity(j => j.ToTable("UserChat"));
    }
}