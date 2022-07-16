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
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, Upgrades);
            new RollOnHover(Actor);
            new DieRenderer(Actor);
            var moneyMaker = new MoneyMaker(Actor);

            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override IBuildCommand[] Commands()
        {
            return Array.Empty<IBuildCommand>();
        }

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
