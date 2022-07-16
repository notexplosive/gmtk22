namespace GMTK22.Data
{
    public abstract class UpgradeModule : Building
    {
        protected UpgradeModule(BuildingPosition position, string name, BuildingMap map) : base(position, name, map, UpgradeModule.Size)
        {
        }
        
        public static int Size { get; } = 32;

        public float SpeedBoost { get; protected set; } = 0;
    }
}
