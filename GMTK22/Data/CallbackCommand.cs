using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public abstract class CallbackCommand : Command
    {
        private readonly Action callback;

        protected CallbackCommand(Action callback)
        {
            this.callback = callback;
        }
        
        public override void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            this.callback();
        }
    }
}
