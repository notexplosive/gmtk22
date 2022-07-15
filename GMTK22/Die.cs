using System.Collections.Generic;

namespace GMTK22
{
    public class Die
    {
        private readonly HashSet<Slot> internalState = new HashSet<Slot>();

        public FillState At(Slot slot)
        {
            return this.internalState.Contains(slot) ? FillState.Filled : FillState.Empty;
        }

        public void Fill(Slot slot)
        {
            if (At(slot) == FillState.Empty)
            {
                this.internalState.Add(slot);
            }
        }

        public void Clear(Slot slot)
        {
            this.internalState.RemoveWhere(t => t == slot);
        }
    }

    public enum FillState
    {
        Empty,
        Filled
    }

    public class DieTransform
    {
    }
}
