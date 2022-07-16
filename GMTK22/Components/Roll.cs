using ExTween;
using Microsoft.Xna.Framework;

namespace GMTK22.Components
{
    public class Roll
    {
        public Roll(int faceValue)
        {
            FaceValue = faceValue;
        }

        public int FaceValue { get; }

        public void ApplyTween(SequenceTween tween, Pip[] inputPips, float duration)
        {
            tween.Add(new DynamicTween(() =>
            {
                var result = new MultiplexTween();
                for (var i = 0; i < inputPips.Length; i++)
                {
                    var inputPip = inputPips[i];
                    var j = i % FaceValue;

                    void TweenTo(Vector2 target)
                    {
                        result.AddChannel(new Tween<Vector2>(inputPip.LocalPosition, target, duration,
                            Ease.QuadFastSlow));
                    }

                    if (j == 0)
                    {
                        if (FaceValue == 1)
                        {
                            TweenTo(Pip.Center);
                        }
                        else
                        {
                            TweenTo(Pip.TopRight);
                        }
                    }
                    else if (j == 1)
                    {
                        TweenTo(Pip.BottomLeft);
                    }
                    else if (j == 2)
                    {
                        if (FaceValue == 6)
                        {
                            TweenTo(Pip.Left);
                        }
                        else if (FaceValue == 4)
                        {
                            TweenTo(Pip.TopLeft);
                        }
                        else
                        {
                            TweenTo(Pip.Center);
                        }
                    }
                    else if (j == 3)
                    {
                        TweenTo(Pip.BottomRight);
                    }
                    else if (j == 4)
                    {
                        TweenTo(Pip.TopLeft);
                    }
                    else if (j == 5)
                    {
                        TweenTo(Pip.Right);
                    }
                }

                return result;
            }));
        }
    }
}
