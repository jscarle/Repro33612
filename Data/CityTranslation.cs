using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class CityTranslation : IEntityTypeConfiguration<CityTranslation>
{
    public CityId CityId { get; set; }
    public City City { get; set; }

    public CultureId TranslationId { get; set; }
    public string Name { get; set; } = "";

    public void Configure(EntityTypeBuilder<CityTranslation> entity)
    {
        entity.HasKey(e => new { e.CityId, e.TranslationId });

        entity.Property(e => e.Name).HasMaxLength(255).IsUnicode(false);

        entity.HasOne(d => d.City).WithMany(p => p.Translations).HasForeignKey(d => d.CityId);
    }
}
