using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly SequenceTween tween = new SequenceTween();
        private readonly BuildingHoverSelectionRenderer buildingHoverSelectionRenderer;
        private readonly Func<SmallBuilding[]> getUpgrades;
        public event Action<Roll> RollFinished;

        public DieComponent(Actor actor, NoiseBasedRNG cleanRandom, Func<SmallBuilding[]> getUpgrades) : base(actor)
        {
            this.getUpgrades = getUpgrades;
            this.cleanRandom = cleanRandom;
            this.buildingHoverSelectionRenderer = RequireComponent<BuildingHoverSelectionRenderer>();

            Pips = new Pip[6];
            for (var i = 0; i < Pips.Length; i++)
            {
                Pips[i] = new Pip();
            }
        }

        public Pip[] Pips { get; }
        public int CurrentFace { get; private set; } = 1;

        public override void Update(float dt)
        {
            this.tween.Update(dt);
        }

        public void ForceRoll()
        {
            // oh god why
            this.tween.Clear();
            this.tween.Reset();
            
            AttemptToRoll();
        }

        public void AttemptToRoll()
        {
            if (this.tween.IsDone())
            {
                // oh god why
                this.tween.Clear();
                this.tween.Reset();
                
                CurrentFace = 0;

                var totalDuration = 1.5f;
                
                // calculate duration
                var upgrades = this.getUpgrades();
                foreach (var upgrade in upgrades)
                {
                    totalDuration -= upgrade.SpeedBoost / 10f;
                }

                // Calculate weight
                var weights = new List<ProbableWeight>();
                var percentForUnweighted = 1f;

                foreach (var upgrade in upgrades)
                {
                    if (!upgrade.ProbableWeight.IsEmpty)
                    {
                        weights.Add(upgrade.ProbableWeight);
                        percentForUnweighted -= upgrade.ProbableWeight.Percentage;
                    }
                }

                var faces = new int[] {1, 2, 3, 4, 5, 6};
                var numberOfFaces = faces.Length;
                var baselineWeight = percentForUnweighted / numberOfFaces;

                var faceToWeight =  new Dictionary<int, float>();
                foreach (var face in faces)
                {
                    faceToWeight[face] = baselineWeight;
                }

                foreach (var weight in weights)
                {
                    faceToWeight[weight.FaceValue] += weight.Percentage;
                }

                var totalPercent = 0f;
                foreach (var val in faceToWeight.Values)
                {
                    totalPercent += val;
                }

                var randomPercent = this.cleanRandom.NextFloat() * totalPercent;

                var accumulatedWeight = 0f;
                Roll roll = null;
                foreach (var faceWeight in faceToWeight)
                {
                    if (accumulatedWeight + faceWeight.Value > randomPercent)
                    {
                        roll = new Roll(faceWeight.Key);
                        break;
                    }
                    accumulatedWeight += faceWeight.Value;
                }

                Debug.Assert(roll != null);
                
                this.tween.Add(new CallbackTween(() =>
                {
                    this.buildingHoverSelectionRenderer.BusyFlags++;
                }));
                
                TweenToRolling(totalDuration * 2 / 3f);

                roll.ApplyTween(this.tween, Pips, totalDuration * 1 / 6f);

                this.tween.Add(new CallbackTween(() =>
                {
                    CurrentFace = roll.FaceValue;
                    RollFinished?.Invoke(roll);
                }));
                this.tween.Add(new WaitSecondsTween(totalDuration * 1 / 6f));
                this.tween.Add(new CallbackTween(() =>
                {
                    this.buildingHoverSelectionRenderer.BusyFlags--;
                }));
            }
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
