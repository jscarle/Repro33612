using Microsoft.EntityFrameworkCore;
using Repro33612.Data;

namespace Repro33612;

public sealed class EfQueryRunner(AppDbContext dbContext)
{
    private readonly Random _random = new();

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var translationId = new CultureId(_random.Next(1, 2));

        var result = await dbContext.Cities
            .Join(
                dbContext.CityTranslations,
                outer => new { outer.CityId, TranslationId = translationId },
                inner => new { inner.CityId, inner.TranslationId },
                (outer, inner) => new { City = outer, CityTranslation = inner }
            )
            .Join(
                dbContext.States,
                outer => outer.City.StateId,
                inner => inner.StateId,
                (outer, inner) => new { outer.City, outer.CityTranslation, State = inner }
            )
            .Join(
                dbContext.StateTranslations,
                outer => new { outer.State.StateId, TranslationId = translationId },
                inner => new { inner.StateId, inner.TranslationId },
                (outer, inner) => new
                {
                    outer.City, outer.CityTranslation, outer.State, StateTranslation = inner,
                }
            )
            .Join(
                dbContext.Countries,
                outer => outer.State.CountryId,
                inner => inner.CountryId,
                (outer, inner) => new
                {
                    outer.City,
                    outer.CityTranslation,
                    outer.State,
                    outer.StateTranslation,
                    Country = inner,
                }
            )
            .Join(
                dbContext.CountryTranslations,
                outer => new { outer.Country.CountryId, TranslationId = translationId },
                inner => new { inner.CountryId, inner.TranslationId },
                (outer, inner) => new
                {
                    outer.City,
                    outer.CityTranslation,
                    outer.State,
                    outer.StateTranslation,
                    outer.Country,
                    CountryTranslation = inner,
                }
            )
            .FirstOrDefaultAsync(cancellationToken);
    }
}
