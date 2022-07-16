using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildingMap
    {
        private readonly Dictionary<BuildingPosition, Building> map;
        public BuildingSelector Selector { get; }

        public BuildingMap(BuildingSelector selector)
        {
            this.map = new Dictionary<BuildingPosition, Building>();
            Selector = selector;
        }

        public void BuildDie(BuildingPosition position)
        {
            new DieBuilding(position, this);

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offset = new Point(x, y);
                    CreateBuildSite(new BuildingPosition(position.GridPosition + offset));
                    CreateUpgradeSite(new BuildingPosition(position.GridPosition,offset));
                }
            }
        }

        private void CreateUpgradeSite(BuildingPosition buildingPosition)
        {
            if (!this.map.ContainsKey(buildingPosition))
            {
                new UpgradeSite(buildingPosition, this);
            }
        }

        private void CreateBuildSite(BuildingPosition gridPosition)
        {
            if (!this.map.ContainsKey(gridPosition))
            {
                new BuildSite(gridPosition, this);
            }
        }

        public void BuildFromCommand(BuildingPosition location, IBuildCommand command)
        {
            Selector.ClearSelection();
            command.Execute(location, this);
            Selector.Select(GetBuildingAt(location));
        }

        private Building GetBuildingAt(BuildingPosition location)
        {
            return this.map[location];
        }

        private bool HasBuildingAt(BuildingPosition location)
        {
            return this.map.ContainsKey(location);
        }

        public void PlaceBuilding(Building building)
        {
            if (HasBuildingAt(building.Position))
            {
                GetBuildingAt(building.Position).Delete();
            }
            this.map[building.Position] = building;
        }
    }
}
