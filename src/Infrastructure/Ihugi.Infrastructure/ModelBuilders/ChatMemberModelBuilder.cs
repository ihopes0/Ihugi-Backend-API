using Ihugi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ihugi.Infrastructure.ModelBuilders;

public class ChatMemberModelBuilder : IEntityTypeConfiguration<ChatMember>
{
    public void Configure(EntityTypeBuilder<ChatMember> builder)
    {
        builder.HasKey(cm => new { cm.ChatId, cm.UserId });
    }
}