using GMTK22.Components;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public abstract class Building
    {
        public Building(Point gridPosition, string name, BuildingMap map)
        {
            GridPosition = gridPosition;
            Name = name;

            Actor = DieCartridge.GameCore.GameScene.AddActor(Name);
            Actor.transform.Position = GridPosition.ToVector2() * 256;
            new BoundingRect(Actor, new Point(128, 128)).SetOffsetToCenter();
            new Hoverable(Actor);
            new Clickable(Actor);
            Selectable = new SelectableBuilding(Actor, this, map.Selector);
            new BuildingHoverSelectionRenderer(Actor);

            map.PlaceBuilding(this);
        }

        public Actor Actor { get; }
        public string Name { get; }
        public abstract IBuildCommand[] Commands { get; }
        public Point GridPosition { get; }
        public SelectableBuilding Selectable { get; }

        public void Delete()
        {
            Actor.Delete();
        }
    }
}
