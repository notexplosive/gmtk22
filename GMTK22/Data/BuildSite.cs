using GMTK22.Components;
using GMTK22.Data.Buildings;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildSite : Building
    {
        public override NameAndDescription NameAndDescription => new NameAndDescription("Build Site", "Can construct dice here");
        
        public BuildSite(PositionAndMap positionAndMap) : base(positionAndMap)
        {
            new BuildSiteRenderer(Actor);
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new ConstructBuildingCommand(WeakDie.Spec),
                new ConstructBuildingCommand(NormalDie.Spec),
                new ConstructBuildingCommand(HighRollDie.Spec),
                new ConstructBuildingCommand(CosmicDie.Spec)
            };
        }
    }
}
