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

            var targetPosition = transform.Position + new Vector2(0, -100);
            
            var position =
                new TweenableVector2(() => transform.Position, val => transform.Position = val);
            this.opacity =
                new TweenableFloat(1);
            
            this.tween = 
                new SequenceTween()
                    .Add(
                        new MultiplexTween()
                            .AddChannel(new Tween<Vector2>(position, targetPosition, 0.75f, Ease.SineFastSlow))
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new WaitSecondsTween(0.25f))
                                    .Add(new Tween<float>(this.opacity, 0, 0.25f, Ease.Linear))
                            )    
                        )
                    .Add(new CallbackTween(()=>this.actor.Destroy()))
                    
                ;
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
            this.text.TextColor = this.originalTextColor.WithMultipliedOpacity(this.opacity.Value);
        }
    }
}
