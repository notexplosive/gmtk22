using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GMTK22
{
    public class Slot
    {
        public static readonly Slot InvalidSlot = new Slot();

        public static readonly Slot TopLeft = new Slot(new Point(0, 0));
        public static readonly Slot TopCenter = new Slot(new Point(1, 0));
        public static readonly Slot TopRight = new Slot(new Point(2, 0));
        public static readonly Slot CenterLeft = new Slot(new Point(0, 1));
        public static readonly Slot CenterCenter = new Slot(new Point(1, 1));
        public static readonly Slot CenterRight = new Slot(new Point(2, 1));
        public static readonly Slot BottomLeft = new Slot(new Point(0, 2));
        public static readonly Slot BottomCenter = new Slot(new Point(1, 2));
        public static readonly Slot BottomRight = new Slot(new Point(2, 2));

        public static readonly Slot[] ValidSlots =
        {
            Slot.TopLeft,
            Slot.TopCenter,
            Slot.TopRight,
            Slot.CenterLeft,
            Slot.CenterCenter,
            Slot.CenterRight,
            Slot.BottomLeft,
            Slot.BottomCenter,
            Slot.BottomRight
        };

        public static readonly Dictionary<Point, Slot> PositionToSlotLookup = new Dictionary<Point, Slot>
        {
            {new Point(0, 0), Slot.TopLeft},
            {new Point(1, 0), Slot.TopCenter},
            {new Point(2, 0), Slot.TopRight},
            {new Point(0, 1), Slot.CenterLeft},
            {new Point(1, 1), Slot.CenterCenter},
            {new Point(2, 1), Slot.CenterRight},
            {new Point(0, 2), Slot.BottomLeft},
            {new Point(1, 2), Slot.BottomCenter},
            {new Point(2, 2), Slot.BottomRight}
        };

        private readonly Point internalPosition;

        private Slot(Point position)
        {
            IsValid = true;
            this.internalPosition = position;
        }
        
        private Slot()
        {
            IsValid = false;
        }

        public Point Position
        {
            get
            {
                Debug.Assert(IsValid);
                return this.internalPosition;
            }
        }

        public bool IsValid { get; }

        public Slot GetAdjacent(Point offset)
        {
            var wasValid = Slot.PositionToSlotLookup.TryGetValue(this.internalPosition + offset, out var result);

            if (wasValid)
            {
                return result;
            }

            return Slot.InvalidSlot;
        }

        public static Slot FromPosition(Point position)
        {
            foreach (var slot in Slot.ValidSlots)
            {
                if (position == slot.Position)
                {
                    return slot;
                }
            }

            return Slot.InvalidSlot;
        }
    }
}
