using System;
using GMTK22.Components;

namespace GMTK22.Data.Buildings
{
    public abstract class BaseDieBuilding : MainBuilding
    {
        protected readonly DieComponent dieComponent;
        
        protected BaseDieBuilding(BuildingPosition position, BuildingMap map, int[] faces, string name, BuildingSpecification spec) : base(position, name, map, faces, spec)
        {
            this.dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, Faces, GetSmallBuildings);
            new RollOnHover(Actor);
            new DieRenderer(Actor, Palette.NormalDieBody, Palette.NormalDiePips);
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
