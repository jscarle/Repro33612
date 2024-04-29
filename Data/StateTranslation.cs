using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repro33612.Data;

public sealed class StateTranslation : IEntityTypeConfiguration<StateTranslation>
{
    public StateId StateId { get; set; }
    public State State { get; set; }

    public CultureId TranslationId { get; set; }
    public string Name { get; set; } = "";

    public void Configure(EntityTypeBuilder<StateTranslation> entity)
    {
        entity.HasKey(e => new { e.StateId, e.TranslationId });

        entity.Property(e => e.Name).HasMaxLength(255).IsUnicode(false);

        entity.HasOne(d => d.State).WithMany(p => p.Translations).HasForeignKey(d => d.StateId);
    }
}
