using System;
using GMTK22.Components;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class ReRoller : UpgradeModule
    {
        public ReRoller(BuildingPosition position, BuildingMap map) : base(position, map, "ReRoller")
        {
            var dieComponent = new DieComponent(Actor, DieCartridge.GameCore.CleanRandom, GetUpgrades);
            new DieRenderer(Actor, Palette.ReRollerBody, Palette.ReRollerPips);
            new ReRollerComponent(Actor, position, Map);
            
            dieComponent.ForceRoll();
            dieComponent.RollFinished += AssignRollingCriteria;
        }

        private void AssignRollingCriteria(Roll roll)
        {
            FaceToReRollOn = roll.FaceValue;
        }

        public int FaceToReRollOn { get; set; } = 0;

        public override IBuildCommand[] Commands()
        {
            return Array.Empty<IBuildCommand>();
        }
    }
    
    public class BuildReRollerCommand : IBuildCommand
    {
        public string Name => "Build ReRoller";
        public void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            new ReRoller(buildingLocation, map);
        }
    }
}
