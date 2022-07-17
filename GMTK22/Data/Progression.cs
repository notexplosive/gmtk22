using ExTween;
using ExTween.MonoGame;
using GMTK22.Data.Buildings;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public class Progression : BaseComponent
    {
        private readonly TweenableFloat cameraZoomTweenable;
        private int phaseIndex;
        private SequenceTween tween;
        private readonly TweenableVector2 cameraPositionTweenable;

        public Progression(Actor actor) : base(actor)
        {
            this.tween = new SequenceTween();
            this.cameraZoomTweenable = new TweenableFloat(() => Camera.Zoom, val => Camera.Zoom = val);
            this.cameraPositionTweenable = new TweenableVector2(() => Camera.UnscaledPosition, val => Camera.UnscaledPosition = val);
        }

        private Camera Camera => this.actor.scene.camera;

        public void StartGame()
        {
            this.phaseIndex = 0;
            Camera.Zoom = 2f;
            Camera.UnscaledPosition = -new Vector2(1600, 900) / 4;
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
        }

        public void OnMoneyChanged(int total, int delta)
        {
            if (total > NormalDie.Spec.Costs.ConstructCost && this.phaseIndex == 0)
            {
                IncreasePhase();
            }
            
            if (total > HighRollDie.Spec.Costs.ConstructCost && this.phaseIndex == 1)
            {
                IncreasePhase();
            }
        }

        private void IncreasePhase()
        {
            // skip tween to end and clear it
            if (this.tween.TotalDuration is KnownTweenDuration knownTweenDuration)
            {
                this.tween.JumpTo(knownTweenDuration);
            }

            this.tween.Clear();

            this.tween = GetTweenForCurrentPhase();
            this.phaseIndex++;
        }

        private SequenceTween GetTweenForCurrentPhase()
        {
            switch (this.phaseIndex)
            {
                case 0:
                    return new SequenceTween()
                            .Add(new MultiplexTween()
                                .AddChannel(new Tween<float>(this.cameraZoomTweenable, 1f, 1f, Ease.QuadFastSlow))
                                .AddChannel(new Tween<Vector2>(this.cameraPositionTweenable, -new Vector2(1600 / 2f, 900 / 2f), 1f, Ease.QuadFastSlow))
                            )
                        ;

                case 1:
                    return new SequenceTween()
                            .Add(new MultiplexTween()
                                .AddChannel(new Tween<float>(this.cameraZoomTweenable, 0.5f, 1f, Ease.QuadFastSlow))
                                .AddChannel(new Tween<Vector2>(this.cameraPositionTweenable, -new Vector2(1600, 900), 1f, Ease.QuadFastSlow))
                            )
                        ;
                
                default:
                    return new SequenceTween();
            }
        }
    }
}
