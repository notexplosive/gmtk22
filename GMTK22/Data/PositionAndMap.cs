namespace GMTK22.Data
{
    public readonly struct PositionAndMap
    {
        public BuildingPosition Position { get; }
        public BuildingMap Map { get; }

        public PositionAndMap(BuildingPosition position, BuildingMap map)
        {
            Position = position;
            Map = map;
        }
    }
}
