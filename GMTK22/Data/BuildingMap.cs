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

        public void CreateBuildSite(BuildingPosition position)
        {
            if (!this.map.ContainsKey(position))
            {
                new BuildSite(position, this);
            }
            
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offset = new Point(x, y);
                    CreateUpgradeSite(new BuildingPosition(position.GridPosition,offset));
                }
            }
        }

        public void BuildFromCommand(BuildingPosition location, Command command)
        {
            if (DieCartridge.GameCore.Player.CanAfford(command.Cost))
            {
                Selector.ClearSelection();
                command.Execute(location, this);
                DieCartridge.GameCore.Player.SpendMoney(command.Cost);
                // Selector.Select(GetBuildingAt(location));
            }
        }

        public Building GetBuildingAt(BuildingPosition location)
        {
            return this.map[location];
        }
        
        public SmallBuilding GetSmallBuildingAt(BuildingPosition location)
        {
            return this.map[location] as SmallBuilding;
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
