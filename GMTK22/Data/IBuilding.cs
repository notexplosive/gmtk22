namespace GMTK22.Data
{
    public interface IBuilding
    {
        string Name { get; }
        IBuildCommand[] Commands { get; }
    }
}
