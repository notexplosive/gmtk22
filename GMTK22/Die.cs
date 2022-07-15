using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace GMTK22
{
    public class Die
    {
        private readonly HashSet<Slot> internalState = new HashSet<Slot>();

        public FillState At(Slot slot)
        {
            if (slot.IsValid)
            {
                return this.internalState.Contains(slot) ? FillState.Filled : FillState.Empty;
            }

            return FillState.Invalid;
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

        public Die Transform(DieTransform dieTransform)
        {
            return dieTransform.GenerateDie(this);
        }
        
        public Die Clone()
        {
            var result = new Die();

            foreach (var pos in this.internalState)
            {
                result.Fill(pos);
            }
            
            return result;
        }

        public IEnumerable<Slot> FilledSlots()
        {
            foreach (var slot in Slot.All)
            {
                if (At(slot) == FillState.Filled)
                {
                    yield return slot;
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var slot in Slot.All)
            {
                sb.Append(At(slot) == FillState.Filled ? "X" : "O");
            }

            return sb.ToString();
        }
    }
}
