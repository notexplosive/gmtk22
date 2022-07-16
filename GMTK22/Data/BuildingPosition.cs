using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GMTK22.Data
{
    public readonly struct BuildingPosition
    {
        public bool Equals(BuildingPosition other)
        {
            return SubgridPosition.Equals(other.SubgridPosition) && GridPosition.Equals(other.GridPosition);
        }

        public override bool Equals(object obj)
        {
            return obj is BuildingPosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SubgridPosition, GridPosition);
        }

        public static bool operator ==(BuildingPosition left, BuildingPosition right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BuildingPosition left, BuildingPosition right)
        {
            return !left.Equals(right);
        }

        public BuildingPosition(Point gridPosition)
        {
            GridPosition = gridPosition;
            SubgridPosition = Point.Zero;
        }
        
        public BuildingPosition(Point gridPosition, Point subgridPosition) : this(gridPosition)
        {
            SubgridPosition = subgridPosition;
        }

        public Point SubgridPosition { get; }
        public Point GridPosition { get; }

        public IEnumerable<BuildingPosition> AllSubgridPositions()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (!(x == 0 && y == 0))
                    {
                        yield return new BuildingPosition(GridPosition, new Point(x, y));
                    }
                }
            }
        }

        public BuildingPosition AsJustGridPosition()
        {
            return new BuildingPosition(GridPosition, Point.Zero);
        }
    }
}
