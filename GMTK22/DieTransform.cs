using System.Collections.Generic;

namespace GMTK22
{
    public abstract class DieTransform
    {
        public TransformResult GenerateDie(Die sourceDie)
        {
            return Apply(sourceDie.Clone());
        }

        public abstract TransformResult Apply(Die original);
    }

    public class MoveDieTransform : DieTransform
    {
        private readonly Direction direction;

        public MoveDieTransform(Direction direction)
        {
            this.direction = direction;
        }

        public override TransformResult Apply(Die original)
        {
            var animation = new AnimationCollection();
            var resultDie = original.Clone();
            var unresolvedSlots = new Queue<Slot>();
            foreach (var slot in original.FilledSlots())
            {
                unresolvedSlots.Enqueue(slot);
            }

            // Nothing to do, just return
            if (unresolvedSlots.Count == 0)
            {
                return new TransformResult(original, animation);
            }

            foreach (var slot in unresolvedSlots)
            {
                resultDie.Fill(slot);
            }

            unresolvedSlots.Enqueue(Slot.Invalid); // invalid slot as a sentinel value

            var lastUnresolvedCount = 0;
            while (unresolvedSlots.Count > 0)
            {
                var currentSlot = unresolvedSlots.Dequeue();
                var isSentinel = currentSlot == Slot.Invalid;

                if (isSentinel)
                {
                    if (lastUnresolvedCount == unresolvedSlots.Count)
                    {
                        // We've circled back around and have not been able to resolve anything
                        break;
                    }

                    lastUnresolvedCount = unresolvedSlots.Count;

                    // Put the sentinel back in
                    unresolvedSlots.Enqueue(currentSlot);
                }
                else
                {
                    var targetSlot = currentSlot.GetAdjacent(this.direction);
                    if (resultDie.At(targetSlot) == FillState.Empty)
                    {
                        animation.RecordMove(currentSlot, targetSlot);
                        resultDie.Clear(currentSlot);
                        resultDie.Fill(targetSlot);
                    }
                    else
                    {
                        // Can't resolve this one right now, push it back
                        unresolvedSlots.Enqueue(currentSlot);
                    }
                }
            }

            return new TransformResult(resultDie, animation);
        }
    }
}
