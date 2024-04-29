using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repro33612.Data;

public record struct CountryId(int Value);

public class CountryIdConverter() : ValueConverter<CountryId, int>(v => v.Value, v => new CountryId(v));
