using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PP.Domain.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IList<Arcana> Arcanas { get; set; } = new List<Arcana>();
}

public class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}