using Machina.Components;
using Machina.Engine;

namespace GMTK22.Components
{
    public class RollOnHover : BaseComponent
    {
        private readonly DieComponent dieComponent;
        private readonly Hoverable hoverable;

        public RollOnHover(Actor actor) : base(actor)
        {
            this.hoverable = RequireComponent<Hoverable>();
            this.dieComponent = RequireComponent<DieComponent>();
        }

        public override void Update(float dt)
        {
            if (this.hoverable.IsHovered)
            {
                this.dieComponent.AttemptToRoll();
            }
        }
    }
}
