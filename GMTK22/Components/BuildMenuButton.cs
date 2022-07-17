using System;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.Engine.Input;
using Microsoft.Xna.Framework.Input;

namespace GMTK22.Components
{
    public class BuildMenuButton : BaseComponent
    {
        private readonly Keys hotkey;
        private readonly Action callback;
        public Command Command { get; }

        public BuildMenuButton(Actor actor, Command command, Building building, BuildMenu menu, Keys hotkey) : base(actor)
        {
            Command = command;
            var clickable = RequireComponent<Clickable>();

            void DoCallback()
            {
                menu.RequestBuilding(building.Position, command);
            }
            
            clickable.OnClick += mouseButton =>
            {
                if (mouseButton == MouseButton.Left)
                {
                    DoCallback();
                }
            };

            this.callback = DoCallback;
            this.hotkey = hotkey;
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (key == this.hotkey && state == ButtonState.Pressed && modifiers.None)
            {
                this.callback();
            }
        }
    }
}
