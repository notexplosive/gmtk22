using System;
using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class NormalDieBuilding : MainBuilding
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build Die",
                info => new NormalDieBuilding(info.Position, info.Map),
                new Costs()
            );
        
        private readonly DieComponent dieComponent;

        public NormalDieBuilding(BuildingPosition position, BuildingMap map) : base(position, "Die", map)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, Upgrades);
            new RollOnHover(Actor);
            new DieRenderer(Actor, Palette.NormalDieBody, Palette.NormalDiePips);
            var moneyMaker = new MoneyMaker(Actor);

            this.dieComponent.RollFinished += moneyMaker.GainMoneyFromRoll;
        }

        public override Command[] Commands()
        {
            return Array.Empty<Command>();
        }

        public override int CurrentFace => this.dieComponent.CurrentFace;

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
