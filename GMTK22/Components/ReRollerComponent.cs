using GMTK22.Data;
using Machina.Components;
using Machina.Engine;

namespace GMTK22.Components
{
    public class ReRollerComponent : BaseComponent
    {
        private readonly DieComponent die;
        private readonly MainBuilding attachedBuilding;
        public BuildingPosition Position { get; }
        public BuildingMap Map { get; }

        public ReRollerComponent(Actor actor, BuildingPosition position, BuildingMap map) : base(actor)
        {
            this.die = RequireComponent<DieComponent>();
            Position = position;
            Map = map;
            this.attachedBuilding = Map.GetMainBuildingAt(Position);
        }

        public override void Update(float dt)
        {
            var building = this.attachedBuilding;
            if (building.IsIdle())
            {
                if (building.CurrentFace == this.die.CurrentFace)
                {
                    building.Roll();
                }
            }
        }
    }
}
