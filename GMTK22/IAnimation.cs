using ExTween;
using GMTK22.Components;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public interface IAnimation
    {
        public Slot FilledSlot { get; }
        public Slot EmptiedSlot { get; }
        AnimationPip BuildTweenAndPip(MultiplexTween tween, DieComponent dieComponent);
    }
    
    public readonly struct MoveAnimation : IAnimation
    {
        public MoveAnimation(Slot start, Slot end)
        {
            EmptiedSlot = start;
            FilledSlot = end;
        }

        public Slot FilledSlot { get; }
        public Slot EmptiedSlot { get; }
        public AnimationPip BuildTweenAndPip(MultiplexTween tween, DieComponent dieComponent)
        {
            var startingPosition = dieComponent.SlotToPosition(EmptiedSlot);
            var endingPosition = dieComponent.SlotToPosition(FilledSlot);
            var pip = new AnimationPip(startingPosition);

            var duration = 0.25f;
            tween
                .AddChannel(
                    new SequenceTween()
                        .Add(new Tween<Vector2>(pip.Position, endingPosition, duration/2, Ease.SineFastSlow))
                        .Add(new Tween<Vector2>(pip.Position, endingPosition, duration/2, Ease.SineSlowFast))
                );

            return pip;
        }
    }
}
