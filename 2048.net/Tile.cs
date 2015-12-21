using System;

namespace _2048.net
{
    public class Tile
    {
        public Tile(CellPosition cell, uint value)
        {
            Position = cell;
            Value = value;
        }

        public MergeTile MergedFrom { get; set; }

        public void SavePosition() {
            _previousPosition = new CellPosition(Position.X, Position.Y);
        }

        public void UpdatePosition(CellPosition position)
        {
            Position = new CellPosition(position.X, Position.Y);
        }

        public CellPosition Position { get; private set; }
        public object Next { get; set; }
        public uint Value { get; private set; }
        private CellPosition _previousPosition;
    }

    public class MergeTile
    {
        public MergeTile(Tile previous, Tile next)
        {
            Previous = previous;
            Next = next;
        }
        public Tile Previous { get; private set; }
        public Tile Next { get; private set; }
    }
}
