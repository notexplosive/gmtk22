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
    public static class PipTweens
    {
        public static void TweenToRolling(SequenceTween tween, Pip[] pips, float totalDuration, PlayableSoundEffect sound)
        {
            tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < pips.Length; i++)
                {
                    var pip = pips[i];

                    var percent = (float) i / pips.Length;
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
                            .Add(new CallbackTween(sound.Play))
                            .Add(new Tween<Vector2>(pip.LocalPosition, startingPos, totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new CallbackTween(sound.Play))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[0], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new CallbackTween(sound.Play))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[1], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                            .Add(new CallbackTween(sound.Play))
                            .Add(new Tween<Vector2>(pip.LocalPosition, orbitPos[2], totalDuration / sequenceItemCount,
                                Ease.QuadFastSlow))
                    );
                }

                return result;
            }));
        }
    }

    public class DieComponent : BaseComponent
    {
        private readonly BuildingHoverSelectionRenderer buildingHoverSelectionRenderer;
        private readonly NoiseBasedRNG cleanRandom;
        private readonly int[] faces;
        private readonly Func<SmallBuilding[]> getUpgrades;
        private readonly SequenceTween tween = new SequenceTween();
        private readonly float baseDuration;
        private readonly PlayableSoundEffect sound;

        public DieComponent(Actor actor, NoiseBasedRNG cleanRandom, int[] faces, float baseDuration, PlayableSoundEffect sound, Func<SmallBuilding[]> getUpgrades) :
            base(actor)
        {
            this.sound = sound;
            this.baseDuration = baseDuration;
            this.faces = faces;
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
        public event Action<Roll> RollFinished;
        public event Action<Vector2> RollStarted;

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
                Progression.NumberOfTotalDieRolls++;
                
                // oh god why
                this.tween.Clear();
                this.tween.Reset();

                CurrentFace = 0;

                var totalDuration = this.baseDuration;

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

                var numberOfFaces = this.faces.Length;
                var baselineWeight = percentForUnweighted / numberOfFaces;

                var faceToWeight = new Dictionary<int, float>();
                foreach (var face in this.faces)
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
                // done with weight calculations
                
                this.tween.Add(new CallbackTween(() =>
                {
                    RollStarted?.Invoke(this.transform.Position);
                }));

                this.tween.Add(new CallbackTween(() => { this.buildingHoverSelectionRenderer.BusyFlags++; }));

                PipTweens.TweenToRolling(this.tween, Pips, totalDuration * 2 / 3f, this.sound);

                Roll.ApplyTween(this.tween, Pips, roll.FaceValue, totalDuration * 1 / 6f);

                this.tween.Add(new CallbackTween(() =>
                {
                    CurrentFace = roll.FaceValue;
                    RollFinished?.Invoke(roll);
                }));
                this.tween.Add(new WaitSecondsTween(totalDuration * 1 / 6f));
                this.tween.Add(new CallbackTween(() => { this.buildingHoverSelectionRenderer.BusyFlags--; }));
            }
        }

        public bool IsTweenDone()
        {
            return this.tween.IsDone();
        }
    }
}
