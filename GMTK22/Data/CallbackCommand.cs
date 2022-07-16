using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public abstract class CallbackCommand : IBuildCommand
    {
        private readonly Action callback;

        protected CallbackCommand(Action callback)
        {
            this.callback = callback;
        }
        
        public abstract string Name { get; }
        public abstract void DrawButtonGraphic(SpriteBatch spriteBatch, Rectangle rectangle, Depth depth);
        public void Execute(BuildingPosition buildingLocation, BuildingMap map)
        {
            this.callback();
        }
    }
}
