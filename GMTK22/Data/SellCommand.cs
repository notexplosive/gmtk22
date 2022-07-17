using Microsoft.Xna.Framework.Audio;

namespace GMTK22.Data
{
    public class SellCommand : Command
    {
        public override NameAndDescription NameAndDescription => new NameAndDescription("Sell", "Sell for a partial refund");
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
            try
            {
                DieCartridge.snapSound.Stop();
                DieCartridge.snapSound.Play();
            }
            catch (InstancePlayLimitException)
            {
                
            }
            
            var building = map.GetBuildingAt(buildingLocation);
            if (building is IHasSpec specHolder)
            {
                building.Sell();
            }
        }
    }
}
