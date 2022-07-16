using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Machina.Engine.Input;

namespace GMTK22.Components
{
    public class SelectableBuilding : BaseComponent
    {
        private readonly BuildingSelector selector;

        public SelectableBuilding(Actor actor, Building building, BuildingSelector selector) : base(actor)
        {
            Building = building;
            this.selector = selector;
            
            var clickable = RequireComponent<Clickable>();
            clickable.OnClick += (mouseButton) =>
            {
                if (mouseButton == MouseButton.Left)
                {
                    this.selector.Select(Building);
                }
            };
        }

        public override void OnActorDestroy()
        {
            if (IsSelected())
            {
                this.selector.ClearSelection();
            }
        }

        public Building Building { get; }

        public bool IsSelected()
        {
            return this.selector.IsSelected(this);
        }
    }
}
