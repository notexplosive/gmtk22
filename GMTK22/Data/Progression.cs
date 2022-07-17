using ExTween;
using ExTween.MonoGame;
using GMTK22.Components;
using GMTK22.Data.Buildings;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Data
{
    public class Progression : BaseComponent
    {
        private readonly TweenableFloat cameraZoomTweenable;
        private int phaseIndex;
        private SequenceTween tween;
        private readonly TweenableVector2 cameraPositionTweenable;
        private readonly TweenableFloat fadeTweenable;
        private readonly Scene gameScene;

        public Progression(Actor actor, Scene gameScene) : base(actor)
        {
            this.gameScene = gameScene;
            this.cameraZoomTweenable = new TweenableFloat(() => Camera.Zoom, val => Camera.Zoom = val);
            this.cameraPositionTweenable = new TweenableVector2(() => Camera.UnscaledPosition, val => Camera.UnscaledPosition = val);
            this.fadeTweenable = new TweenableFloat(0);
            this.tween = new SequenceTween();
        }

        private Camera Camera => this.gameScene.camera;
        public static int NumberOfTotalDieRolls { get; set; }

        private static bool firstDie;
        public static bool IsFirstDie()
        {
            if (!Progression.firstDie)
            {
                Progression.firstDie = true;
                return true;
            }

            return false;
        }

        public void StartGame()
        {
            GameStarted = true;
            this.phaseIndex = 0;

            this.tween = new SequenceTween()
                    .Add(new MultiplexTween()
                        .AddChannel(new Tween<Vector2>(this.cameraPositionTweenable, -new Vector2(1600, 900) / 4, 1.5f, Ease.QuadFastSlow))
                    )
                ;
        }

        public bool GameStarted { get; private set; }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new RectangleF(0,0,1920, 1080), Palette.CosmicDieBody.WithMultipliedOpacity(this.fadeTweenable.Value), 0f);
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

        public void LoadEndScene()
        {
            var sceneLayers = this.gameScene.sceneLayers;
            sceneLayers.RemoveScene(this.gameScene);
            sceneLayers.RemoveScene(this.actor.scene);

            sceneLayers.BackgroundColor = Palette.CosmicDieBody;
            var endScene = sceneLayers.AddNewScene();

            new Ending(endScene.AddActor("EndingRunner"));
        }

        public void TriggerEndCutscene(Vector2 target)
        {
            Camera.ZoomTarget = () => target;
            this.tween = new SequenceTween()
                    .Add(new MultiplexTween()
                        .AddChannel(new Tween<float>(this.cameraZoomTweenable, 20f, 5f, Ease.Linear))
                        .AddChannel(new SequenceTween()
                            .Add(new WaitSecondsTween(2f))
                            .Add(new Tween<float>(this.fadeTweenable, 1f, 2f, Ease.QuadSlowFast))
                            .Add(new CallbackTween(LoadEndScene))
                        )
                    )
                ;
        }
    }
}
