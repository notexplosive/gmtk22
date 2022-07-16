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
}
