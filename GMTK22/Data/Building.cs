﻿using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public abstract class Building
    {
        protected Building(BuildingPosition position, string name, BuildingMap map, int buildingSize = 128)
        {
            Position = position;
            Name = name;
            Map = map;

            Actor = DieCartridge.GameCore.GameScene.AddActor(Name);
            Actor.transform.Position = Position.GridPosition.ToVector2() * (256 + 64) +
                                       Position.SubgridPosition.ToVector2() * (64 + 32);
            new BoundingRect(Actor, new Point(buildingSize, buildingSize)).SetOffsetToCenter();
            new Hoverable(Actor);
            new Clickable(Actor);
            Selectable = new SelectableBuilding(Actor, this, map.Selector);
            new BuildingHoverSelectionRenderer(Actor);

            map.PlaceBuilding(this);
        }

        public BuildingMap Map { get; }
        public Actor Actor { get; }
        public string Name { get; }
        public BuildingPosition Position { get; }
        public SelectableBuilding Selectable { get; }
        public abstract Command[] Commands();
        
        public void Destroy()
        {
            Actor.Destroy();
        }

        public void Sell()
        {
            if (this is IHasSpec specContainer)
            {
                var costs = specContainer.MySpec.Costs;
                var sellValue = costs.SellValue;
                if (sellValue > 0)
                {
                    MoneyMaker.GainMoney(sellValue, Actor.transform.Position);
                }

                if (this is MainBuilding mainBuilding)
                {
                    foreach (var upgrade in mainBuilding.GetSmallBuildings())
                    {
                        upgrade.Sell();
                    }

                    new BuildSite(Position, Map);
                }
                else
                {
                    new UpgradeSite(Position, Map);
                }
            }
        }
    }
}
