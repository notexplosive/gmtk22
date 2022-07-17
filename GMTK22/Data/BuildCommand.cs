using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public abstract class Command
    {
        public abstract NameAndDescription NameAndDescription { get; }
        public abstract int Cost { get; }
        public abstract void Execute(BuildingPosition buildingLocation, BuildingMap map);

        public abstract void DrawButtonGraphic(DrawInfo drawInfo);
    }
}
