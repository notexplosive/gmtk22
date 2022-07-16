namespace GMTK22.Data
{
    public class SellCommand : Command
    {
        public override string Name => "Sell";
        public override int Cost { get; }

        public SellCommand(Building building)
        {
            if (building is IHasSpec specHolder)
            {
                Cost = -specHolder.MySpec.Costs.SellValue;
            }
        }
        
        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            var building = map.GetBuildingAt(buildingLocation);
            if (building is IHasSpec specHolder)
            {
                building.Sell();
            }
        }

        public override void DrawButtonGraphic(DrawInfo drawInfo)
        {
            
        }
    }
}
