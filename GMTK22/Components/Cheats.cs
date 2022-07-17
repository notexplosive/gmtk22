using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework.Input;

namespace GMTK22.Components
{
    public class Cheats : BaseComponent
    {
        public Cheats(Actor actor) : base(actor)
        {   
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (state == ButtonState.Pressed && modifiers.None)
            {
                if (key == Keys.G)
                {
                    DieCartridge.GameCore.Player.GainMoney(500);
                }
            }
        }
    }
}
