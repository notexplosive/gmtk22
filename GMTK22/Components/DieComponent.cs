using System;
using System.Collections.Generic;
using ExTween;
using GMTK22.Data;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class DieComponent : BaseComponent
    {
        private readonly NoiseBasedRNG cleanRandom;
        private readonly Hoverable hoverable;
        private readonly Player player;
        private readonly int size;
        private readonly SequenceTween tween = new SequenceTween();
        private readonly BuildingHoverSelectionRenderer buildingHoverSelectionRenderer;
        private readonly Func<UpgradeModule[]> getUpgrades;

        public DieComponent(Actor actor, Player player, NoiseBasedRNG cleanRandom, Func<UpgradeModule[]> getUpgrades) : base(actor)
        {
            this.getUpgrades = getUpgrades;
            this.player = player;
            this.cleanRandom = cleanRandom;
            this.size = RequireComponent<BoundingRect>().Height;
            this.buildingHoverSelectionRenderer = RequireComponent<BuildingHoverSelectionRenderer>();

            Pips = new Pip[6];
            for (var i = 0; i < Pips.Length; i++)
            {
                Pips[i] = new Pip();
            }

            this.hoverable = RequireComponent<Hoverable>();
        }

        public Pip[] Pips { get; }

        public override void Update(float dt)
        {
            this.tween.Update(dt);

            if (this.hoverable.IsHovered)
            {
                AttemptToRoll();
            }
        }

        public void AttemptToRoll()
        {
            if (this.tween.IsDone())
            {
                // oh god why
                this.tween.Clear();
                this.tween.Reset();

                var totalDuration = 1.5f;

                foreach (var upgrade in this.getUpgrades())
                {
                    totalDuration -= upgrade.SpeedBoost / 10f;
                }

                this.tween.Add(new CallbackTween(() =>
                {
                    this.buildingHoverSelectionRenderer.BusyFlags++;
                }));
                
                TweenToRolling(totalDuration * 2 / 3f);

                var roll = this.cleanRandom.GetRandomElement(new[]
                {
                    new Roll(1),
                    new Roll(2),
                    new Roll(3),
                    new Roll(4),
                    new Roll(5),
                    new Roll(6)
                });

                roll.ApplyTween(this.tween, Pips, totalDuration * 1 / 6f);

                this.tween.Add(new CallbackTween(() =>
                {
                    SpawnMoneyTextParticle(roll.FaceValue);
                    this.player.GainMoney(roll.FaceValue);
                }));
                this.tween.Add(new WaitSecondsTween(totalDuration * 1 / 6f));
                this.tween.Add(new CallbackTween(() =>
                {
                    this.buildingHoverSelectionRenderer.BusyFlags--;
                }));
            }
        }

        private void SpawnMoneyTextParticle(int value)
        {
            var moneyCounter = this.actor.transform.AddActorAsChild("TextParticle");

            moneyCounter.transform.LocalDepth = -10;
            
            new BoundingRect(moneyCounter, 5, 5).SetOffsetToCenter();
            var text = new BoundedTextRenderer(moneyCounter, "0", MachinaClient.Assets.GetSpriteFont("UIFont"), Color.White,
                Alignment.Center, Overflow.Ignore);
            text.Text = $"+{value}";
            text.TextColor = Color.Goldenrod;

            new TextToastTween(moneyCounter);
        }

        public void TweenToRolling(float totalDuration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Length; i++)
                {
                    var pip = Pips[i];

                    var percent = (float) i / Pips.Length;
                    var fullCircle = MathF.PI * 2;
                    var startingPos = new Vector2(MathF.Cos(percent * fullCircle), MathF.Sin(percent * fullCircle));

                    var orbitPos = new[]
                    {
                        new Vector2(MathF.Cos((percent + 0.25f) * fullCircle),
                            MathF.Sin((percent + 0.25f) * fullCircle)),
                        new Vector2(MathF.Cos((percent + 0.5f) * fullCircle), MathF.Sin((percent + 0.5f) * fullCircle)),
                        new Vector2(MathF.Cos((percent + 0.75f) * fullCircle),
                            MathF.Sin((percent + 0.75f) * fullCircle))
                    };

                    var sequenceItemCount = 4f;
                    result.AddChannel(
                        new SequenceTween()
                            .Add(new Tween<Vector2>(pip.LocalPosition, startingPos, totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[0], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[1], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[2], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                    );
                }

                return result;
            }));
        }

        public bool IsTweenDone()
        {
            return this.tween.IsDone();
        }
    }
}
