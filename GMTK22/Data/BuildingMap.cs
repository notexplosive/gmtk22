using System;
using System.Collections.Generic;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Machina.Engine.Input;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildingMap
    {
        private readonly GameCore gameCore;
        private readonly Dictionary<Point, IBuilding> map;
        private readonly BuildingSelector selector;

        public BuildingMap(GameCore gameCore, BuildingSelector selector)
        {
            this.map = new Dictionary<Point, IBuilding>();
            this.gameCore = gameCore;
            this.selector = selector;
        }

        public void CreateDie(Point gridPosition)
        {
            var die = CreateBuildingActor(gridPosition, new DieBuilding());
            new DieComponent(die, this.gameCore.Player, this.gameCore.CleanRandom);
            new DieRenderer(die);

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var offsetGridPosition = gridPosition + new Point(x, y);
                    CreateBuildSite(offsetGridPosition);
                }
            }

        }

        private Actor CreateBuildingActor(Point gridPosition, IBuilding building)
        {
            var buildingActor = this.gameCore.GameScene.AddActor(building.Name);
            buildingActor.transform.Position = gridPosition.ToVector2() * 256;
            new BoundingRect(buildingActor, new Point(128, 128)).SetOffsetToCenter();
            new Hoverable(buildingActor);
            new Clickable(buildingActor);
            new SelectableBuilding(buildingActor, building, this.selector);
            new BuildingHoverSelectionRenderer(buildingActor);
            
            this.map[gridPosition] = building;

            
            return buildingActor;
        }

        private void CreateBuildSite(Point gridPosition)
        {
            if (!this.map.ContainsKey(gridPosition))
            {
                var site = CreateBuildingActor(gridPosition, new BuildSite());
                new BuildSiteComponent(site);
                new BuildSiteRenderer(site);
            }
        }
    }

    public class DieBuilding : IBuilding
    {
        public string Name => "Die";

        public IBuildCommand[] Commands => Array.Empty<IBuildCommand>();
    }

    public class BuildSite : IBuilding
    {
        public string Name => "Build Site";

        public IBuildCommand[] Commands => new IBuildCommand[]
        {
            new BuildDieCommand()
        };
    }
}
