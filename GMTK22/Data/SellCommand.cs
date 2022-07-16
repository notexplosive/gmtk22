namespace GMTK22.Data
{
    public class SellCommand : Command
    {
        public override string Name => "Sell";
        public override int Cost => 0;

        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            var building = map.GetBuildingAt(buildingLocation);
            building.Sell();
        }

        public override void DrawButtonGraphic(DrawInfo drawInfo)
        {
            
        }
    }
}
