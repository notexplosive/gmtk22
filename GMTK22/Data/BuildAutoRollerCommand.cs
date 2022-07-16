using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class BuildAutoRollerCommand : IBuildCommand
    {
        public string Name => "Build Auto Roller";
        public void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            new AutoRoller(buildingLocation, map);
        }
    }

    public class BuildSpeedUpgradeCommand : IBuildCommand
    {
        public string Name => "Build Speed Module";
        public void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            new SpeedUpgrade(buildingLocation, map);
        }
    }
    
    public class BuildWeightModuleCommand : IBuildCommand
    {
        public string Name => "Build Weight Module";
        public void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            new WeightModule(buildingLocation, map);
        }
    }
}
