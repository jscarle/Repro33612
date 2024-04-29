using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Repro33612.Data;

public record struct StateId(int Value);

public class StateIdConverter() : ValueConverter<StateId, int>(v => v.Value, v => new StateId(v));
