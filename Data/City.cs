using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class City : IEntityTypeConfiguration<City>
{
    public CityId CityId { get; set; }
    public ICollection<CityTranslation> Translations { get; init; }

    public StateId StateId { get; set; }
    public State State { get; init; }

    public void Configure(EntityTypeBuilder<City> entity)
    {
        entity.HasKey(e => e.CityId);

        entity.HasOne(d => d.State).WithMany(p => p.Cities).HasForeignKey(d => d.StateId);
    }
}
