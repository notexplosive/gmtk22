﻿using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildSite : Building
    {
        public BuildSite(BuildingPosition position, BuildingMap map) : base(position, "Build Site", map)
        {
            new BuildSiteRenderer(Actor);
        }

        public override IBuildCommand[] Commands => new IBuildCommand[]
        {
            new BuildDieCommand()
        };
    }

    public class UpgradeSite : Building
    {
        public UpgradeSite(BuildingPosition position, BuildingMap map) : base(position, "Upgrade Site", map, buildingSize: 32)
        {
            new BuildSiteRenderer(Actor);
        }

        public override IBuildCommand[] Commands => new IBuildCommand[]
        {
        };
    }
}