using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public interface IBuildCommand
    {
        string Name { get; }

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth);

        public void Execute(BuildingPosition buildingLocation, BuildingMap map);
    }

    public class BuildDieCommand : IBuildCommand
    {
        public string Name => "Build Die";

        public void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth)
        {
            spriteBatch.FillRectangle(rectangle, Color.White, depth);
            var radius = rectangle.Width / 10;
            spriteBatch.DrawCircle(new CircleF(rectangle.Center, radius), 8, Color.Black, radius, depth - 1);
            var offsetSize = rectangle.Width / 5;
            spriteBatch.DrawCircle(new CircleF(rectangle.Center.ToVector2() + new Vector2(-offsetSize), radius), 8, Color.Black, radius, depth - 1);
            spriteBatch.DrawCircle(new CircleF(rectangle.Center.ToVector2() + new Vector2(offsetSize), radius), 8, Color.Black, radius, depth - 1);
            spriteBatch.DrawCircle(new CircleF(rectangle.Center.ToVector2() + new Vector2(-offsetSize,offsetSize), radius), 8, Color.Black, radius, depth - 1);
            spriteBatch.DrawCircle(new CircleF(rectangle.Center.ToVector2() + new Vector2(offsetSize, -offsetSize), radius), 8, Color.Black, radius, depth - 1);
        }

        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            map.BuildDie(buildingLocation);
        }
    }
}
