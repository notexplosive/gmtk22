using GMTK22.Data;
using Machina.Components;
using Machina.Engine;

namespace GMTK22.Components
{
    public class ReRollerComponent : BaseComponent
    {
        private readonly DieComponent die;
        public BuildingPosition Position { get; }
        public BuildingMap Map { get; }

        public ReRollerComponent(Actor actor, BuildingPosition position, BuildingMap map) : base(actor)
        {
            this.die = RequireComponent<DieComponent>();
            Position = position;
            Map = map;
        }

        public override void Update(float dt)
        {
            var building = GetAttachedBuilding();
            if (building.IsIdle())
            {
                if (building.CurrentFace == this.die.CurrentFace)
                {
                    building.Roll();
                }
            }
        }

        public MainBuilding GetAttachedBuilding()
        {
            return Map.GetMainBuildingAt(Position);
        }
    }
}
