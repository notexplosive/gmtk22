﻿using GMTK22.Components;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class ReRollerModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build ReRoller",
                info => new ReRollerModule(info.Position, info.Map),
                new Costs()
            );
        
        private readonly DieComponent dieComponent;

        public ReRollerModule(BuildingPosition position, BuildingMap map) : base(position, map, "ReRoller", ReRollerModule.Spec)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, GetUpgrades);
            new DieRenderer(Actor, Palette.ReRollerBody, Palette.ReRollerPips);
            new ReRollerComponent(Actor, position, Map);
            
            dieComponent.ForceRoll();
            dieComponent.RollFinished += AssignRollingCriteria;
        }

        private void AssignRollingCriteria(Roll roll)
        {
            FaceToReRollOn = roll.FaceValue;
        }

        public int FaceToReRollOn { get; set; } = 0;

        public override Command[] Commands()
        {
            return new Command[]
            {
                new ReRollCommand(() => { this.dieComponent.ForceRoll(); })
            };
        }
    }
}
