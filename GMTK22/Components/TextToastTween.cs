using ExTween;
using ExTween.MonoGame;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class TextToastTween : BaseComponent
    {
        private readonly BoundedTextRenderer text;
        private readonly ITween tween;
        private readonly Color originalTextColor;
        private readonly TweenableFloat opacity;

        public TextToastTween(Actor actor) : base(actor)
        {
            this.text = RequireComponent<BoundedTextRenderer>();
            this.originalTextColor = this.text.TextColor;

            var position =
                new TweenableVector2(() => transform.LocalPosition, val => transform.LocalPosition = val);
            this.opacity =
                new TweenableFloat(1);
            
            this.tween = 
                    new MultiplexTween()
                        .AddChannel(new Tween<Vector2>(position, new Vector2(0, -100), 0.75f, Ease.SineFastSlow))
                        .AddChannel(
                            new SequenceTween()
                                .Add(new WaitSecondsTween(0.25f))
                                .Add(new Tween<float>(this.opacity, 0, 0.25f, Ease.Linear))
                            
                            )
                ;
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
            this.text.TextColor = this.originalTextColor.WithMultipliedOpacity(this.opacity.Value);
        }
    }
}
