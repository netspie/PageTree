namespace PageTree.Domain.EC
{
    public class ECObject
    {
        public List<ECComponent> Components { get; } = new List<ECComponent>();

        public void Add<T>() where T : ECComponent, new() =>
            Components.Add(new T());

        public T Get<T>() where T : ECComponent, new() =>
            Components.OfType<T>().FirstOrDefault();

        public T[] GetMany<T>() where T : ECComponent, new() =>
            Components.OfType<T>().ToArray();
    }
}