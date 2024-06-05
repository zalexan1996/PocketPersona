using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PP.Domain.Entities;

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int GameId { get; set; }
    public virtual Game Game { get; set; }
    public required IList<string> Gifts { get; set; }
}

public class CharacterConfig : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(X => X.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Gifts)
            .IsUnicode(false)
            .HasMaxLength(50);
            
        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasOne(x => x.Game)
            .WithMany()
            .HasForeignKey(x => x.GameId);

    }
}