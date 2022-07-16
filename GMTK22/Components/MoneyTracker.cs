using ExTween;
using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class MoneyTracker : BaseComponent
    {
        private readonly Player player;
        private ITween tween;
        private readonly TweenableInt tweenable;
        private readonly BoundedTextRenderer text;

        public MoneyTracker(Actor actor, Player player) : base(actor)
        {
            this.text = RequireComponent<BoundedTextRenderer>();
            this.player = player;
            this.tweenable = new TweenableInt();

            this.text.EnableDropShadow(Color.Black);

            this.player.MoneyChanged += OnMoneyChange;
        }

        private void OnMoneyChange(int total, int delta)
        {
            this.tween = new Tween<int>(this.tweenable, total, 0.25f, Ease.QuadFastSlow);
        }

        public override void Update(float dt)
        {
            this.tween?.Update(dt);
            this.text.Text = this.tweenable.Value.ToString() + " pips";
        }
    }
}
