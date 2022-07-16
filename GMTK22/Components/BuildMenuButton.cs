using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Machina.Engine.Input;

namespace GMTK22.Components
{
    public class BuildMenuButton : BaseComponent
    {
        private readonly IBuildCommand command;
        private readonly Building building;
        private readonly BuildMenu menu;
        private readonly Clickable clickable;

        public BuildMenuButton(Actor actor, IBuildCommand command, Building building, BuildMenu menu) : base(actor)
        {
            this.clickable = RequireComponent<Clickable>();
            
            this.command = command;
            this.building = building;
            this.menu = menu;

            this.clickable.OnClick += mouseButton =>
            {
                if (mouseButton == MouseButton.Left)
                {
                    this.menu.RequestBuilding(building.GridPosition, command);
                }
            };
        }
    }
}
