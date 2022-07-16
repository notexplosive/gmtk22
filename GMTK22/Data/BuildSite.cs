using GMTK22.Components;
using GMTK22.Data.Buildings;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildSite : Building
    {
        public BuildSite(PositionAndMap positionAndMap) : base(positionAndMap, "Build Site")
        {
            new BuildSiteRenderer(Actor);
        }

        public override Command[] Commands()
        {
            return new Command[]
            {
                new ConstructBuildingCommand(WeakDie.Spec),
                new ConstructBuildingCommand(NormalDie.Spec)
            };
        }
    }
}
