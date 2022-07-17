using ExTween;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
        public class UpgradeBuildingBodyRenderer : BaseComponent
        {
            private readonly BoundingRect boundingRect;
            private readonly ITwoColor twoColor;
            private readonly TweenableFloat sizeTweenable;
            private readonly SequenceTween tween;

            public UpgradeBuildingBodyRenderer(Actor actor, ITwoColor twoColor) : base(actor)
            {
                this.twoColor = twoColor;
                this.boundingRect = RequireComponent<BoundingRect>();
                
                this.sizeTweenable = new TweenableFloat(1);
                this.tween = new SequenceTween()
                    .Add(new Tween<float>(this.sizeTweenable, 1.5f, 0.15f, Ease.SineFastSlow))
                    .Add(new Tween<float>(this.sizeTweenable, 1f, 0.25f, Ease.SineSlowFast));
            }

            public override void Update(float dt)
            {
                this.tween.Update(dt);
            }
            
            public void TriggerPulse()
            {
                if (this.tween.IsDone())
                {
                    this.tween.Reset();
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                var radius = this.boundingRect.Rect.Width / 2f * this.sizeTweenable.Value;
                spriteBatch.DrawCircle(this.boundingRect.Rect.Center.ToVector2(), radius, 24, this.twoColor.PrimaryColor, radius, transform.Depth + 50);
                spriteBatch.DrawCircle(this.boundingRect.Rect.Center.ToVector2(), radius, 24, this.twoColor.SecondaryColor, 4f, transform.Depth + 40);
            }
        }
}
