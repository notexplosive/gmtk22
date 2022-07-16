using System.Collections.Generic;
using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class BuildingMap
    {
        private readonly Dictionary<Point, IBuilding> map;
        private readonly GameCore gameCore;

        public BuildingMap(GameCore gameCore)
        {
            this.gameCore = gameCore; 
            this.map = new Dictionary<Point, IBuilding>();
        }

        public void CreateDie(Point gridPosition)
        {
            var die = CreateBuildingActor(gridPosition, "Die");
            new DieComponent(die, gameCore.Player, gameCore.CleanRandom);
            new DieRenderer(die);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    var offsetGridPosition = gridPosition + new Point(x, y);
                    CreateBuildSite(offsetGridPosition);
                }
            }
            
            this.map[gridPosition] = new DieBuilding(die);
        }

        private Actor CreateBuildingActor(Point gridPosition, string name)
        {
            var building = this.gameCore.GameScene.AddActor(name);
            building.transform.Position = gridPosition.ToVector2() * 256;
            new BoundingRect(building, new Point(128, 128)).SetOffsetToCenter();
            new Hoverable(building);
            return building;
        }

        private void CreateBuildSite(Point gridPosition)
        {
            if (!this.map.ContainsKey(gridPosition))
            {
                var site = CreateBuildingActor(gridPosition, "Build Site");
                new BuildSiteComponent(site);
                new BuildSiteRenderer(site);
            }
        }

        public class DieBuilding : IBuilding
        {
            private readonly Actor actor;

            public DieBuilding(Actor actor)
            {
                this.actor = actor;
            }
        }
    }
}
