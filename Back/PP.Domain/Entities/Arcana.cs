using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PP.Domain.Entities;

public class Arcana
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required IList<Game> Games { get; set; } = new List<Game>();
}

public class ArcanaConfig : IEntityTypeConfiguration<Arcana>
{
    public void Configure(EntityTypeBuilder<Arcana> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}