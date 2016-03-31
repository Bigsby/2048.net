using System;
using System.Collections.Generic;
using System.Linq;

namespace DCCC
{
    public class GameGrid
    {
        private GameTile[,] _cells;
        private int _size;

        private GameGrid(int size)
        {
            _size = size;
        }

        public GameGrid(int size, GameGrid previousState = null)
            : this(size)
        {
            _cells = null == previousState ? BuildEmpty() : BuildFromPreviousState(previousState);

            //for (int xIndex = 0; xIndex < size; xIndex++)
            //    for (int yIndex = 0; yIndex < size; yIndex++)
            //        _cells[xIndex, yIndex].UpdatePosition(new CellPosition(xIndex, yIndex));
        }

        public GameGrid(int size, GameTile[,] tiles)
             : this(size)
        {
            _cells = tiles;
        }

        public int Size { get { return _size; } }

        // Check if there are any cells available
        public bool CellsAvailable()
        {
            return AvailableCells().Any();
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

        public void InsertTile(GameTile tile)
        {
            Cells[tile.Position.X, tile.Position.Y] = tile;
        }

        public void RemoveTile(GameTile tile)
        {
            Cells[tile.Position.X, tile.Position.Y] = null;
        }

        // Call callback for every cell
        public void EachCell(Action<int, int, GameTile> callback)
        {
            for (var x = 0; x < _size; x++)
            {
                for (var y = 0; y < _size; y++)
                {
                    callback(x, y, _cells[x, y]);
                }
            }
        }

        public GameTile CellContent(CellPosition position)
        {
            return CellContent(position.X, position.Y);
        }

        public GameTile CellContent(int x, int y)
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

        public GameTile[,] Cells
        { get { return _cells; } }

        public GameTile[,] BuildFromPreviousState(GameGrid state)
        {
            var cells = BuildEmpty();

            for (var x = 0; x < _size; x++)
                for (var y = 0; y < _size; y++)
                {
                    var tile = state.Cells[x, y];
                    if (null != tile)
                        cells[x, y] = new GameTile(new CellPosition(x, y), tile.Value);
                }

            return cells;
        }

        // Build a grid of the specified size
        public GameTile[,] BuildEmpty()
        {
            var cells = new GameTile[_size, _size];

            return cells;
        }

        private IEnumerable<CellPosition> AvailableCells()
        {
            var result = new List<CellPosition>();
            EachCell((x, y, tile) =>
            {
                if (null == tile)
                    result.Add(new CellPosition(x, y));
            });

            return result;
        }

        private bool CellOccupied(CellPosition position)
        {
            return null != Cells[position.X, position.Y];
        }
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

        public override string ToString()
        {
            return string.Format("[{0},{1}]", X, Y);
        }
    }
}
