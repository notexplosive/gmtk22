using GMTK22.Components;
using Machina.Engine;

namespace GMTK22.Data
{
    public class BuildingSelector
    {
        private readonly BuildMenu buildMenu;
        private SelectableBuilding selected;

        public BuildingSelector(BuildMenu buildMenu)
        {
            this.buildMenu = buildMenu;
        }

        public void Select(SelectableBuilding selectable)
        {
            this.selected = selectable;
            this.buildMenu.BuildButtons(selectable.Building.Commands);
        }

        public bool IsSelected(SelectableBuilding selectableBuilding)
        {
            return this.selected == selectableBuilding;
        }

        public void ClearSelection()
        {
            this.selected = null;
        }
    }
}
