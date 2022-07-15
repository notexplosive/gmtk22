using System.Collections.Generic;

namespace GMTK22
{
    public class AnimationCollection
    {
        private readonly List<IAnimation> animations = new List<IAnimation>();

        public void RecordMove(Slot start, Slot end)
        {
            this.animations.Add(new MoveAnimation(start, end));
        }

        public IEnumerator<IAnimation> Each()
        {
            return this.animations.GetEnumerator();
        }

        public IAnimation[] All()
        {
            return this.animations.ToArray();
        }
    }
}
