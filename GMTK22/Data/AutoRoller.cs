using System;
using GMTK22.Components;

namespace GMTK22.Data
{
    public class AutoRoller : UpgradeModule
    {
        private readonly AutoRollerComponent roller;

        public AutoRoller(BuildingPosition position, BuildingMap map) : base(position, "Auto Roller", map)
        {
            this.roller = new AutoRollerComponent(Actor, map.GetMainBuildingAt(new BuildingPosition(position.GridPosition)));
        }

        public override IBuildCommand[] Commands { get; } = Array.Empty<IBuildCommand>();
    }
}
