using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class CallbackCommand : Command
    {
        private readonly Action callback;

        public CallbackCommand(string name, int cost, Action callback)
        {
            this.callback = callback;
            Name = name;
            Cost = cost;
        }

        public override string Name { get; }
        public override int Cost { get; }

        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            this.callback();
        }

        public override void DrawButtonGraphic(DrawInfo drawInfo)
        {
        }
    }
}
