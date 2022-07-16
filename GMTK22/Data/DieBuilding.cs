using System;
using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class DieBuilding : Building
    {
        public DieBuilding(BuildingPosition position, BuildingMap map) : base(position, "Die", map)
        {
            new DieComponent(Actor, DieCartridge.GameCore.Player, DieCartridge.GameCore.CleanRandom);
            new DieRenderer(Actor);
        }

        public override IBuildCommand[] Commands => Array.Empty<IBuildCommand>();
    }
}
