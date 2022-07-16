using System.Collections.Generic;
using ExTween;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class DieComponent : BaseComponent
    {
        private Die die;
        private readonly Dictionary<Slot, Actor> actorMap;
        private TweenAnimation pendingAnimation;

        public DieComponent(Actor actor, Dictionary<Slot, Actor> actorMap, Die startingDie) : base(actor)
        {
            this.die = startingDie.Clone();
            this.actorMap = actorMap;
        }

        public override void Update(float dt)
        {
            if (this.pendingAnimation != null)
            {
                this.pendingAnimation.Update(dt);
                if (this.pendingAnimation.IsDone())
                {
                    this.pendingAnimation = null;
                }
            }
        }

        public override void OnKey(Keys key, ButtonState state, ModifierKeys modifiers)
        {
            if (state == ButtonState.Pressed && modifiers.None)
            {
                if (key == Keys.Left || key == Keys.Right || key == Keys.Down || key == Keys.Up)
                {
                    if (this.pendingAnimation == null)
                    {
                        var transformResult = this.die.Transform(new MoveDieTransform(Direction.FromKey(key)));
                        this.die = transformResult.Die;
                        this.pendingAnimation = transformResult.GenerateTween(this);
                    }
                }
            }
        }

        public Vector2 SlotToPosition(Slot slot)
        {
            if (!slot.IsValid)
            {
                return Vector2.Zero;
            }
            
            return this.actorMap[slot].transform.Position;
        }

        public IEnumerable<Vector2> PipPositions()
        {
            if (this.pendingAnimation == null)
            {
                foreach (var slot in this.die.FilledSlots())
                {
                    yield return SlotToPosition(slot);
                }
            }
            else
            {
                foreach (var pip in this.pendingAnimation.Pips)
                {
                    yield return pip.Position;
                }
            }
        }
    }
}
