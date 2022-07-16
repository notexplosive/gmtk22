using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Machina.Engine.Input;

namespace GMTK22.Components
{
    public class BuildMenuButton : BaseComponent
    {

        public BuildMenuButton(Actor actor, IBuildCommand command, Building building, BuildMenu menu) : base(actor)
        {
            var clickable = RequireComponent<Clickable>();

            clickable.OnClick += mouseButton =>
            {
                if (mouseButton == MouseButton.Left)
                {
                    menu.RequestBuilding(building.Position, command);
                }
            };
        }
    }
}
