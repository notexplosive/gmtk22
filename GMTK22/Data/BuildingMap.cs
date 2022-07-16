using System.Collections.Generic;
using GMTK22.Components;
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

        public void CreateUpgradeSite(BuildingPosition buildingPosition)
        {
            if (!this.map.ContainsKey(buildingPosition))
            {
                new UpgradeSite(buildingPosition, this);
            }
        }

        public void CreateBuildSite(BuildingPosition gridPosition)
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

        public Building GetBuildingAt(BuildingPosition location)
        {
            return this.map[location];
        }
        
        public UpgradeModule GetUpgradeAt(BuildingPosition location)
        {
            return this.map[location] as UpgradeModule;
        }
        
        public MainBuilding GetMainBuildingAt(BuildingPosition location)
        {
            return this.map[new BuildingPosition(location.GridPosition)] as MainBuilding;
        }

        private bool HasBuildingAt(BuildingPosition location)
        {
            return this.map.ContainsKey(location);
        }

        public void PlaceBuilding(Building building)
        {
            if (HasBuildingAt(building.Position))
            {
                GetBuildingAt(building.Position).Destroy();
            }
            this.map[building.Position] = building;
        }
    }
}
