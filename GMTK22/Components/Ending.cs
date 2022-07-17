using ExTween;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GMTK22.Components
{
    public class Ending : BaseComponent
    {
        private readonly TweenableFloat creditsOpacity;
        private readonly TweenableFloat endTitleOpacity;
        private readonly Pip[] pips;
        private readonly SequenceTween tween;

        public Ending(Actor actor) : base(actor)
        {
            this.endTitleOpacity = new TweenableFloat();
            this.creditsOpacity = new TweenableFloat();

            this.pips = new Pip[7]
            {
                new Pip(),
                new Pip(),
                new Pip(),
                new Pip(),
                new Pip(),
                new Pip(),
                new Pip()
            };

            this.pips[0].LocalPosition.ForceSetValue(Pip.BottomLeft);
            this.pips[1].LocalPosition.ForceSetValue(Pip.Right);
            this.pips[2].LocalPosition.ForceSetValue(Pip.TopLeft);
            this.pips[3].LocalPosition.ForceSetValue(Pip.BottomRight);
            this.pips[4].LocalPosition.ForceSetValue(Pip.TopRight);
            this.pips[5].LocalPosition.ForceSetValue(Pip.Left);
            this.pips[6].LocalPosition.ForceSetValue(Pip.Center);

            foreach (var pip in this.pips)
            {
                pip.RadiusPercent.ForceSetValue(0);
            }

            var bgmVolumeTweenable =
                new TweenableFloat(() => DieCartridge.bgm.Volume, val => DieCartridge.bgm.Volume = val);
            this.tween = new SequenceTween();
            this.tween.Add(new Tween<float>(bgmVolumeTweenable, 0, 1f, Ease.Linear));
            this.tween.Add(new WaitSecondsTween(1));

            var pipIndex = 0;
            foreach (var pip in this.pips)
            {
                var index = pipIndex;
                this.tween.Add(new CallbackTween(() =>
                {
                    if (index == 6)
                    {
                        MachinaClient.SoundEffectPlayer.PlaySound("growflower", 1f, useCache: false);
                    }
                    else
                    {
                        MachinaClient.SoundEffectPlayer.PlaySound("grow", 1f);
                    }
                }));
                this.tween.Add(new Tween<float>(pip.RadiusPercent, 1f, 0.4f, Ease.QuadFastSlow));
                this.tween.Add(new WaitSecondsTween(0.25f));
                pipIndex++;
            }

            this.tween.Add(new WaitSecondsTween(2));
            this.tween.Add(new Tween<float>(this.endTitleOpacity, 1f, 1f, Ease.Linear));
            this.tween.Add(new WaitSecondsTween(1));
            this.tween.Add(new Tween<float>(this.creditsOpacity, 1f, 1f, Ease.Linear));
        }

        public override void Update(float dt)
        {
            this.tween.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var center = new Vector2(1600, 900) / 2;

            foreach (var pip in this.pips)
            {
                var pipPosition = pip.LocalPosition.Value * 300f + center;
                var radius = pip.RadiusPercent * 30f;

                if (radius > 0)
                {
                    spriteBatch.DrawCircle(new CircleF(pipPosition, radius), 25, Palette.CosmicDiePips, radius);
                    spriteBatch.DrawCircle(new CircleF(pipPosition, radius + 10), 25, Palette.CosmicDiePips, 3);
                }
            }

            var titleFont = MachinaClient.Assets.GetSpriteFont("UIFont");
            var titleText = "You rolled a 7!";
            var titleSize = titleFont.MeasureString(titleText);
            spriteBatch.DrawString(titleFont, titleText, center + new Vector2(0, -400f),
                Palette.CosmicDiePips.WithMultipliedOpacity(this.endTitleOpacity.Value), 0f, titleSize / 2f,
                Vector2.One,
                SpriteEffects.None, 0f);

            var creditsFont = MachinaClient.Assets.GetSpriteFont("UIFontSmall");
            var creditsText = "By NotExplosive and Quarkimo - notexplosive.net - soundcloud.com/quarkimo";
            var creditsSize = creditsFont.MeasureString(creditsText);
            spriteBatch.DrawString(creditsFont, creditsText, center + new Vector2(0, 400f),
                Palette.CosmicDiePips.WithMultipliedOpacity(this.creditsOpacity.Value), 0f, creditsSize / 2f,
                Vector2.One,
                SpriteEffects.None, 0f);
        }
    }
}
