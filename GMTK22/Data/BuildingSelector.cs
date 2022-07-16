using GMTK22.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

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

        public void Select(Building building)
        {
            this.selected = building.Selectable;
            this.buildMenu.PopulateButtons(building);
        }

        public bool IsSelected(SelectableBuilding selectableBuilding)
        {
            return this.selected == selectableBuilding;
        }

        public void ClearSelection()
        {
            this.selected = null;
            this.buildMenu.ClearButtons();
        }
    }
}
