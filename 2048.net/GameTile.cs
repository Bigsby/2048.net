namespace DCCC
{
    public class GameTile
    {
        public GameTile(CellPosition cell, uint value)
        {
            Position = cell;
            Value = value;
        }

        public MergeTile MergedFrom { get; set; }
        public bool IsNew { get; set; }
        public CellPosition PreviousPosition { get { return _previousPosition; } }

        public void SavePosition()
        {
            _previousPosition = new CellPosition(Position.X, Position.Y);
        }

        public void UpdatePosition(CellPosition position)
        {
            Position = position;
        }

        public CellPosition Position { get; private set; }
        public object Next { get; set; }
        public uint Value { get; private set; }
        private CellPosition _previousPosition;

        public override string ToString()
        {
            return Position + " " + Value;
        }
    }

    public class MergeTile
    {
        public MergeTile(CellPosition previous, CellPosition next)
        {
            Previous = previous;
            Next = next;
        }
        public CellPosition Previous { get; private set; }
        public CellPosition Next { get; private set; }

        public override string ToString()
        {
            return Previous + " » " + Next;
        }
    }
}
