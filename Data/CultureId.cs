using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repro33612.Data;

public record struct CultureId(int Value);

public class CultureIdConverter() : ValueConverter<CultureId, int>(v => v.Value, v => new CultureId(v));
