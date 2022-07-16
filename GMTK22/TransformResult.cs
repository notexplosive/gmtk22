using System.Collections.Generic;
using ExTween;
using ExTween.MonoGame;
using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public readonly struct TransformResult
    {
        public TransformResult(Die die, AnimationCollection animation)
        {
            Die = die;
            Animation = animation;
        }

        public Die Die { get; }
        public AnimationCollection Animation { get; }

        public TweenAnimation GenerateTween(DieComponent dieComponent)
        {
            return new TweenAnimation(this, dieComponent);
        }
    }

    public class Pip
    {
        public Pip(Vector2 startingPosition)
        {
            Position.ForceSetValue(startingPosition);
        }

        public TweenableVector2 Position { get; } = new TweenableVector2();
    }

    public class TweenAnimation
    {
        private readonly MultiplexTween rootTween = new MultiplexTween();
        public List<Pip> Pips { get; } = new List<Pip>();

        public TweenAnimation(TransformResult result, DieComponent dieComponent)
        {
            var staticSlots = new HashSet<Slot>();

            foreach (var slot in result.Die.FilledSlots())
            {
                staticSlots.Add(slot);
            }

            foreach (var animation in result.Animation.All())
            {
                if (animation.FilledSlot.IsValid)
                {
                    staticSlots.Remove(animation.FilledSlot);
                }
            }

            foreach (var slot in staticSlots)
            {
                Pips.Add(new Pip(dieComponent.SlotToPosition(slot)));
            }

            foreach (var animation in result.Animation.All())
            {
                var pip = animation.BuildTweenAndPip(this.rootTween, dieComponent);
                Pips.Add(pip);
            }
        }

        public void Update(float dt)
        {
            this.rootTween.Update(dt);
        }

        public bool IsDone()
        {
            return this.rootTween.IsDone();
        }
    }
}
