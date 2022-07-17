using ExTween;
using Machina.Components;
using Machina.Engine;

namespace GMTK22.Components
{
    public class Ending : BaseComponent
    {
        private readonly SequenceTween tween;

        public Ending(Actor actor) : base(actor)
        {
            this.tween = new SequenceTween();
        }
    }
}
