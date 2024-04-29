using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class CountryTranslation : IEntityTypeConfiguration<CountryTranslation>
{
    public CountryId CountryId { get; set; }
    public Country Country { get; set; }

    public CultureId TranslationId { get; set; }
    public string Name { get; set; } = "";

    public void Configure(EntityTypeBuilder<CountryTranslation> entity)
    {
        entity.HasKey(e => new { e.CountryId, e.TranslationId });

        entity.Property(e => e.Name).HasMaxLength(255).IsUnicode(false);

        entity.HasOne(d => d.Country).WithMany(p => p.Translations).HasForeignKey(d => d.CountryId);
    }
}
