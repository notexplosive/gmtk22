using System;
using Machina.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTK22.Data
{
    public class ReRollCommand : CallbackCommand
    {
        public ReRollCommand(Action callback) : base(callback)
        {
        }

        public override string Name => "ReRoll";
        
        public override void DrawButtonGraphic(DrawInfo drawInfo)
        {
            
        }
    }
}
