﻿using GMTK22.Components;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class SellCommand : IBuildCommand
    {
        public string Name => "Sell";
        public void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            var building = map.GetBuildingAt(buildingLocation);

            building.Sell();
        }
    }
}
