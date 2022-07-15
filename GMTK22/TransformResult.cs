namespace GMTK22
{
    public readonly struct TransformResult
    {
        public TransformResult(Die die, AnimationCollection animation)
        {
            Die = die;
            Animation = animation;
        }

        public Die Die { get; }
        public AnimationCollection Animation { get; }
    }
}
