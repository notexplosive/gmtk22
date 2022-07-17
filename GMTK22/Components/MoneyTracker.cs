using ExTween;
using ExTween.MonoGame;
using GMTK22.Data;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class MoneyTracker : BaseComponent
    {
        private readonly Player player;
        private readonly Vector2 startingPosition;
        private readonly BoundedTextRenderer text;
        private readonly TweenableInt tweenableNumberValue;
        private readonly TweenableVector2 tweenablePositionOffset;
        private ITween tween;

        public MoneyTracker(Actor actor, Player player) : base(actor)
        {
            this.text = RequireComponent<BoundedTextRenderer>();
            this.player = player;
            this.tweenableNumberValue = new TweenableInt();
            this.tweenablePositionOffset = new TweenableVector2(new Vector2(0, -500));
            this.startingPosition = transform.Position;

            this.text.EnableDropShadow(Color.Black);

            this.player.MoneyChanged += OnMoneyChange;

            this.tween = new SequenceTween()
                    .Add(new WaitSecondsTween(0.5f))
                    .Add(new Tween<Vector2>(this.tweenablePositionOffset, Vector2.Zero, 1f, Ease.QuadFastSlow))
                ;
            
            transform.Position = this.startingPosition + this.tweenablePositionOffset;
        }

        private void OnMoneyChange(int total, int delta)
        {
            this.tween = new Tween<int>(this.tweenableNumberValue, total, 0.25f, Ease.QuadFastSlow);
        }

        public override void Update(float dt)
        {
            if (DieCartridge.GameCore.Progression.GameStarted)
            {
                this.tween?.Update(dt);
            }

            this.text.Text = this.tweenableNumberValue.Value + " pips";

            transform.Position = this.startingPosition + this.tweenablePositionOffset;
        }
    }
}
