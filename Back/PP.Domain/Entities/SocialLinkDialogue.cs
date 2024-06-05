using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PP.Domain.Entities;

public class SocialLinkDialogue
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public int Rank { get; set; }
    public int Order { get; set; }

    public int SocialLinkId { get; set; }
    public SocialLink SocialLink { get; set; }
}

public class SocialLinkDialogueConfig : IEntityTypeConfiguration<SocialLinkDialogue>
{
    public void Configure(EntityTypeBuilder<SocialLinkDialogue> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(X => X.Text).HasMaxLength(255);
        
        builder.HasOne(x => x.SocialLink)
            .WithMany(x => x.Dialogues)
            .HasForeignKey(x => x.SocialLinkId);
    }
}