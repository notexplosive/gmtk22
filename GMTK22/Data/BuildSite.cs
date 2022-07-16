using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildSite : Building
    {
        public BuildSite(BuildingPosition position, BuildingMap map) : base(position, "Build Site", map)
        {
            new BuildSiteRenderer(Actor);
        }

        public override IBuildCommand[] Commands()
        {
            return new IBuildCommand[]
            {
                new BuildDieCommand()
            };
        }
    }
}
