namespace FluxorTest.Models
{
    public record  Doodad : IIdentifiedObject, INamedObject
    {
        public required Guid Id { get; init; }

        public required string Name { get; init; }
    }
}
