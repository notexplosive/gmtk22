using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class CallbackCommand : Command
    {
        private readonly Action callback;

        public CallbackCommand(NameAndDescription nameAndDescription, int cost, Action callback)
        {
            this.callback = callback;
            NameAndDescription = nameAndDescription;
            Cost = cost;
        }

        public override NameAndDescription NameAndDescription { get; }
        public override int Cost { get; }

        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            this.callback();
        }
    }
}
