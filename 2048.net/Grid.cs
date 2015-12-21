using System;
using System.Collections.Generic;
using System.Linq;

namespace _2048.net
{
    public class Grid
    {
        private Tile[,] _cells;
        private int _size;

        public Grid(int size, Grid previousState = null)
        {
            _size = size;
            _cells = null == previousState ? BuildEmpty() : BuildFromPreviousState(previousState);
        }

        public int Size { get; set; }

        // Check if there are any cells available
        public bool CellsAvailable()
        {
            return !AvailableCells().Any();
        }

        // Find the first available random position
        public CellPosition RandomAvailableCell()
        {
            var cells = AvailableCells();

            if (cells.Any())
            {
                return cells.Skip(new Random().Next(0, cells.Count() - 1)).First();
            }

            return new CellPosition();
        }

        public void InsertTile(Tile tile)
        {
            Cells[tile.Position.X, tile.Position.Y] = tile;
        }

        public void RemoveTile(Tile tile)
        {
            Cells[tile.Position.X, tile.Position.Y] = null;
        }

        // Call callback for every cell
        public void EachCell(Action<int, int, Tile> callback)
        {
            for (var x = 0; x < _size; x++)
            {
                for (var y = 0; y < _size; y++)
                {
                    callback(x, y, _cells[x, y]);
                }
            }
        }

        public Tile CellContent(CellPosition position)
        {
            return CellContent(position.X, position.Y);
        }

        public Tile CellContent(int x, int y)
        {
            if (WithinBounds(x, y))
            {
                return Cells[x, y];
            }
            else {
                return null;
            }
        }

        public bool WithinBounds(CellPosition position)
        {
            return WithinBounds(position.X, position.Y);
        }

        public bool WithinBounds(int x, int y)
        {
            return x >= 0 && x < _size &&
                 y >= 0 && y < _size;
        }

        // Check if the specified cell is taken
        public bool CellAvailable(CellPosition position)
        {
            return !CellOccupied(position);
        }

        public Tile[,] Cells
        { get { throw new NotImplementedException(); } }

        public Tile[,] BuildFromPreviousState(Grid state)
        {
            var cells = BuildEmpty();

            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                {
                    var tile = state.Cells[x, y];
                    cells[x, y] = new Tile(tile.Position, tile.Value);
                }

            return cells;
        }

        // Build a grid of the specified size
        public Tile[,] BuildEmpty()
        {
            var cells = new Tile[_size, _size];

            return cells;
        }

        private IEnumerable<CellPosition> AvailableCells()
        {
            var result = new List<CellPosition>();
            EachCell((x, y, tile) =>
            {
                if (null == tile)
                    result.Add(tile.Position);
            });

            return result;
        }

        private bool CellOccupied(CellPosition position)
        {
            return null != Cells[position.X, position.Y];
        }
    }

    public class Cell
    {
    }

    public struct CellPosition
    {
        public CellPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool IsEqual(CellPosition pos)
        {
            return X == pos.X && Y == pos.Y;
        }
    }
}
