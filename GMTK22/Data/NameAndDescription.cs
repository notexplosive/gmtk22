namespace GMTK22.Data
{
    public readonly struct NameAndDescription
    {
        public string Name { get; }
        public string Description { get; }

        public NameAndDescription(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
