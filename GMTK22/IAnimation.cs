namespace GMTK22
{
    public interface IAnimation
    {
        public Slot FilledSlot { get; }
        public Slot EmptiedSlot { get; }
    }
    
    public readonly struct MoveAnimation : IAnimation
    {
        public MoveAnimation(Slot start, Slot end)
        {
            EmptiedSlot = start;
            FilledSlot = end;
        }

        public Slot FilledSlot { get; }
        public Slot EmptiedSlot { get; }
    }
}
