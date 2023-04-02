namespace FluxorTest.Models
{
    public abstract record GenericListState<T>
    {
        public bool Initialized { get; init; } = false;
        public bool Loading { get; init; } = false;
        public List<T> Items { get; init; } = new();
    }
}
