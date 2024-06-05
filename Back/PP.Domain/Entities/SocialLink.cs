using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PP.Domain.Entities;

public class SocialLink
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string UnlockConditions { get; set; }

    public int CharacterId { get; set; }
    public int ArcanaId { get; set; }
    public Character Character { get; set; }
    public Arcana Arcana { get; set; }
    public IList<SocialLinkDialogue> Dialogues { get; set; }
}

public class SocialLinkConfig : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UnlockConditions);

        builder.Property(X => X.Name).HasMaxLength(255);

        builder.HasOne(x => x.Character)
            .WithOne()
            .HasForeignKey(nameof(SocialLink), nameof(SocialLink.CharacterId));

        builder.HasOne(X => X.Arcana)
            .WithMany()
            .HasForeignKey(x => x.ArcanaId);
    }
}