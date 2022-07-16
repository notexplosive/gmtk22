using System;
using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class DieBuilding : MainBuilding
    {
        private readonly DieComponent dieComponent;

        public DieBuilding(BuildingPosition position, BuildingMap map) : base(position, "Die", map)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.Player, DieCartridge.GameCore.CleanRandom, ()=> Upgrades);
            new DieRenderer(Actor);
        }

        public override IBuildCommand[] Commands => Array.Empty<IBuildCommand>();
        public override bool IsIdle()
        {
            return this.dieComponent.IsTweenDone();
        }
        public override void Roll()
        {
            this.dieComponent.AttemptToRoll();
        }
    }
}
