using GMTK22.Data;
using Machina.Components;
using Machina.Engine;

namespace GMTK22.Components
{
    public class UpgradeSiteRenderer : BaseComponent
    {
        private readonly BuildingPosition position;
        private readonly BuildingMap map;

        public UpgradeSiteRenderer(Actor actor, BuildingPosition buildingPosition, BuildingMap map) : base(actor)
        {
            this.position = buildingPosition;
            this.map = map;
            
            if (this.map.GetBuildingAt(this.position.AsJustGridPosition()) is BuildSite)
            {
                this.actor.Visible = false;
            }
            else
            {
                this.actor.Visible = true;
            }
        }

        public override void Update(float dt)
        {
            if (this.map.GetBuildingAt(this.position.AsJustGridPosition()) is BuildSite)
            {
                this.actor.Visible = false;
            }
            else
            {
                this.actor.Visible = true;
            }
        }
    }
}
