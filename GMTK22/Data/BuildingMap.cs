using System;
using System.Collections.Generic;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildingMap
    {
        private readonly Dictionary<Point, Building> map;
        public BuildingSelector Selector { get; }

        public BuildingMap(BuildingSelector selector)
        {
            this.map = new Dictionary<Point, Building>();
            this.Selector = selector;
        }

        public void BuildDie(Point gridPosition)
        {
            var die = new DieBuilding(gridPosition, this);
            new DieComponent(die.Actor, DieCartridge.GameCore.Player, DieCartridge.GameCore.CleanRandom);
            new DieRenderer(die.Actor);

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offsetGridPosition = gridPosition + new Point(x, y);
                    CreateBuildSite(offsetGridPosition);
                }
            }
        }

        private void CreateBuildSite(Point gridPosition)
        {
            if (!this.map.ContainsKey(gridPosition))
            {
                var site = new BuildSite(gridPosition, this);
                new BuildSiteComponent(site.Actor);
                new BuildSiteRenderer(site.Actor);
            }
        }

        public void BuildFromCommand(Point location, IBuildCommand command)
        {
            this.Selector.ClearSelection();
            command.Execute(location, this);
            this.Selector.Select(GetBuildingAt(location));
        }

        private Building GetBuildingAt(Point location)
        {
            return this.map[location];
        }

        private bool HasBuildingAt(Point location)
        {
            return this.map.ContainsKey(location);
        }

        public void PlaceBuilding(Building building)
        {
            if (HasBuildingAt(building.GridPosition))
            {
                GetBuildingAt(building.GridPosition).Delete();
            }
            this.map[building.GridPosition] = building;
        }
    }

    public class DieBuilding : Building
    {
        public DieBuilding(Point gridPosition, BuildingMap map) : base(gridPosition, "Die", map)
        {
        }

        public override IBuildCommand[] Commands => Array.Empty<IBuildCommand>();
    }

    public class BuildSite : Building
    {
        public BuildSite(Point gridPosition, BuildingMap map) : base(gridPosition, "Build Site", map)
        {
        }

        public override IBuildCommand[] Commands => new IBuildCommand[]
        {
            new BuildDieCommand()
        };
    }
}
