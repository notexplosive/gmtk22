using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract void Execute(BuildingPosition buildingLocation, BuildingMap map);

        public abstract void DrawButtonGraphic(DrawInfo drawInfo);
    }
}
