namespace GMTK22
{
    public interface IAnimation
    {
        
    }
    
    public readonly struct MoveAnimation : IAnimation
    {
        private readonly Slot start;
        private readonly Slot end;

        public MoveAnimation(Slot start, Slot end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
