using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repro33612.Data;

public record struct CityId(int Value);

public class CityIdConverter() : ValueConverter<CityId, int>(v => v.Value, v => new CityId(v));
