using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class Country : IEntityTypeConfiguration<Country>
{
    public CountryId CountryId { get; set; }
    public ICollection<CountryTranslation> Translations { get; init; }

    public ICollection<State> States { get; init; }

    public void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.HasKey(e => e.CountryId);
    }
}
