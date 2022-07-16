using System;
using System.Collections.Generic;
using ExTween;
using ExTween.MonoGame;
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
        private readonly int size;
        private readonly SequenceTween tween = new SequenceTween();
        private readonly Player player;

        public DieComponent(Actor actor, Player player, NoiseBasedRNG cleanRandom) : base(actor)
        {
            this.player = player;
            this.cleanRandom = cleanRandom;
            this.size = RequireComponent<BoundingRect>().Height;
            for (var i = 0; i < 6; i++)
            {
                var pip = new Pip();
                Pips.Add(pip);
            }

            this.hoverable = RequireComponent<Hoverable>();
        }

        public List<Pip> Pips { get; } = new List<Pip>();

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

                TweenToRolling(totalDuration * 2 / 3f);

                var queueRollAnimation = this.cleanRandom.GetRandomElement(new Action<float>[]
                {
                    TweenToOne,
                    TweenToTwo,
                    TweenToThree,
                    TweenToFour,
                    TweenToFive,
                    TweenToSix
                });

                queueRollAnimation(totalDuration * 1 / 6f);

                this.tween.Add(new CallbackTween(() =>
                {
                    this.player.GainMoney(5);
                }));
                this.tween.Add(new WaitSecondsTween(totalDuration * 1 / 6f));
            }
        }

        public void TweenToRolling(float totalDuration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];

                    var percent = (float) i / Pips.Count;
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

        public void TweenToOne(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                foreach (var pip in Pips)
                {
                    result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Vector2.Zero, duration, Ease.QuadFastSlow));
                }

                return result;
            }));
        }

        public void TweenToTwo(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];
                    var j = i % 2;

                    if (j == 0)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 1)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomLeft, duration,
                            Ease.QuadFastSlow));
                    }
                }

                return result;
            }));
        }

        public void TweenToThree(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];
                    var j = i % 3;

                    if (j == 0)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 1)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 2)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.Center, duration,
                            Ease.QuadFastSlow));
                    }
                }

                return result;
            }));
        }

        public void TweenToFour(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];
                    var j = i % 4;

                    if (j == 0)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 1)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 2)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 3)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomRight, duration,
                            Ease.QuadFastSlow));
                    }
                }

                return result;
            }));
        }

        public void TweenToFive(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];
                    var j = i % 5;

                    if (j == 0)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 1)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 2)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 3)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 4)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.Center, duration,
                            Ease.QuadFastSlow));
                    }
                }

                return result;
            }));
        }

        public void TweenToSix(float duration)
        {
            this.tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();

                for (var i = 0; i < Pips.Count; i++)
                {
                    var pip = Pips[i];
                    var j = i % 6;

                    if (j == 0)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 1)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 2)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.TopLeft, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 3)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.BottomRight, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 4)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.Left, duration,
                            Ease.QuadFastSlow));
                    }
                    else if (j == 5)
                    {
                        result.AddChannel(new Tween<Vector2>(pip.LocalPosition, Pip.Right, duration,
                            Ease.QuadFastSlow));
                    }
                }

                return result;
            }));
        }
    }

    public class Pip
    {
        public static readonly Vector2 TopRight = new Vector2(1, -1);
        public static readonly Vector2 BottomLeft = new Vector2(-1, 1);
        public static readonly Vector2 BottomRight = new Vector2(1, 1);
        public static readonly Vector2 TopLeft = new Vector2(-1, -1);
        public static readonly Vector2 Center = new Vector2(0, 0);
        public static readonly Vector2 Left = new Vector2(-1, 0);
        public static readonly Vector2 Right = new Vector2(1, 0);
        public TweenableVector2 LocalPosition { get; } = new TweenableVector2();
        public TweenableInt IsVisible { get; } = new TweenableInt(1);
    }
}
