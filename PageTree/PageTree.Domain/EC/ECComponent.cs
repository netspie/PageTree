namespace PageTree.Domain.EC
{
    public class ECComponent
    {
        public string Type { get; private set; }

        public ECComponent()
        {
            Type = Type ?? GetType().Name;
        }
    }
}