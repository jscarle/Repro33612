using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class State : IEntityTypeConfiguration<State>
{
    public StateId StateId { get; set; }
    public ICollection<StateTranslation> Translations { get; init; }

    public CountryId CountryId { get; set; }
    public Country Country { get; init; }

    public ICollection<City> Cities { get; init; }

    public void Configure(EntityTypeBuilder<State> entity)
    {
        entity.HasKey(e => e.StateId);

        entity.HasOne(d => d.Country).WithMany(p => p.States).HasForeignKey(d => d.CountryId);
    }
}
