﻿using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public class ReRollerModule : UpgradeModule
    {
        public static readonly BuildingSpecification Spec =
            new BuildingSpecification("Build ReRoller",
                info => new ReRollerModule(info.Position, info.Map),
                new Costs(100)
            );
        
        private readonly DieComponent dieComponent;

        public ReRollerModule(BuildingPosition position, BuildingMap map) : base(position, map, "ReRoller", ReRollerModule.Spec)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, AttachedBuilding.Faces, GetUpgrades);
            new DieRenderer(Actor, Palette.ReRollerBody, Palette.ReRollerPips);
            new ReRollerComponent(Actor, position, Map);
            
            this.dieComponent.ForceRoll();
            this.dieComponent.RollFinished += AssignRollingCriteria;
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
                new CallbackCommand("ReRoll", ReRollerModule.Spec.Costs.ConstructCost / 2,() => { this.dieComponent.ForceRoll(); })
            };
        }
    }
}
