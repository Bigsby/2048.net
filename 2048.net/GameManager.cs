using DCCC.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DCCC
{
    internal class GameManager : IGameState
    {
        private IUIManager _inputManager;
        private ILocalStorageManager _localStorageManager;
        private int _size;
        private int _startTiles = 2;
        private readonly int _winingTileValue;

        private GameGrid _grid;

        public GameManager(int size, IUIManager inputManager, ILocalStorageManager localStorageManager)
        {
            if (size < 4 || size > 8)
                throw new ArgumentOutOfRangeException("size", "Size must be between 4 and 8");

            _size = size;
            _winingTileValue = CalculateWiningTileValue(size);
            _inputManager = inputManager;
            _localStorageManager = localStorageManager;

            _inputManager.OnMove(HandleMove);
            _inputManager.OnRestart(HandleRestart);
            _inputManager.OnKeepPlaying(HandleKeepPlaying);

            _inputManager.Ready += (s, e) => Setup();
        }

        #region Properties
        public uint Score { get; set; }
        public uint BestScore { get; set; }
        public bool Over { get; set; }
        public bool Won { get; set; }
        public bool KeepPlaying { get; set; }
        public GameGrid Grid
        {
            get
            {
                return _grid;
            }
            set
            {
                _grid = value;
            }
        }
        #endregion

        #region Private Methods

        #region Handle UI Events
        private void HandleMove(MoveDirection direction)
        {
            InternalMove(direction);
        }

        // Restart the game
        private void HandleRestart()
        {
            _localStorageManager.ClearGameState();
            _inputManager.ContinueGame();
            Setup();
        }

        // Keep playing after winning (allows going over 2048)
        private void HandleKeepPlaying()
        {
            KeepPlaying = true;
            _inputManager.ContinueGame(); // Clear the game won/lost message
        }
        #endregion

        #region Game methods
        // Adds a tile in a random position
        private void AddRandomTile()
        {
            if (_grid.CellsAvailable())
            {
                var value = (uint)(((double)(new Random().Next(0, 10000)) / 10000) < 0.9 ? 2 : 4);
                var tile = new GameTile(_grid.RandomAvailableCell(), value) { IsNew = true };

                _grid.InsertTile(tile);
            }
        }

        // Return true if the game is lost, or has won and the user hasn't kept playing
        private bool IsGameTerminated()
        {
            return Over || (Won && !KeepPlaying);
        }

        #region Move
        // Move tiles on the grid in the specified direction
        private void InternalMove(MoveDirection direction)
        {
            if (IsGameTerminated()) return; // Don't do anything if the game's over

            GameTile tile = null;

            var vector = GetVector(direction);
            var traversals = new Traversals(_size, vector);
            var moved = false;

            // Save the current tile positions and remove merger information
            PrepareTiles();

            // Traverse the grid in the right direction and move tiles
            foreach (var x in traversals.Xs)
            {
                foreach (var y in traversals.Ys)
                {
                    var cell = new CellPosition(x, y);
                    tile = _grid.CellContent(cell);

                    if (null != tile)
                    {
                        var positions = FindFarthestPosition(cell, vector);
                        var next = _grid.CellContent(positions.Next);

                        // Only one merger per row traversal?
                        if (null != next && next.Value == tile.Value && (null == next.MergedFrom))
                        {
                            var merged = new GameTile(positions.Next, tile.Value * 2);
                            merged.MergedFrom = new MergeTile(tile.Position, next.Position);

                            _grid.InsertTile(merged);
                            _grid.RemoveTile(tile);

                            // Converge the two tiles' positions
                            tile.UpdatePosition(positions.Next);

                            // Update the score
                            Score += merged.Value;

                            // The mighty 2048 tile
                            if (merged.Value == _winingTileValue) Won = true;
                        }
                        else
                            MoveTile(tile, positions.Farthest);

                        if (!cell.IsEqual(tile.Position))
                            moved = true; // The tile moved from its original cell!
                    }
                }
            }

            if (moved)
            {
                AddRandomTile();

                if (!MovesAvailable())
                {
                    Over = true; // Game over!
                }

                Actuate();
            }
        }

        private void MoveTile(GameTile tile, CellPosition cell)
        {
            _grid.Cells[tile.Position.X, tile.Position.Y] = null;
            _grid.Cells[cell.X, cell.Y] = tile;
            tile.UpdatePosition(cell);
        }

        // Save all tile positions and remove merger info
        private void PrepareTiles()
        {
            _grid.EachCell((x, y, tile) =>
            {
                if (null != tile)
                {
                    tile.MergedFrom = null;
                    tile.SavePosition();
                    tile.IsNew = false;
                }
            });
        }

        private bool MovesAvailable()
        { return _grid.CellsAvailable() || TileMatchesAvailable(); }

        private bool TileMatchesAvailable()
        {
            GameTile tile;

            for (var x = 0; x < _size; x++)
            {
                for (var y = 0; y < _size; y++)
                {
                    tile = _grid.CellContent(x, y);

                    if (null != tile)
                    {
                        for (var direction = 0; direction < 4; direction++)
                        {
                            var vector = GetVector((MoveDirection)direction);

                            var other = _grid.CellContent(x + vector.X, y + vector.Y);

                            if (null != other && other.Value == tile.Value)
                            {
                                return true; // These two tiles can be merged
                            }
                        }
                    }
                }
            }

            return false;
        }

        // Get the vector representing the chosen direction
        private Vector GetVector(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return new Vector(0, -1);
                case MoveDirection.Right:
                    return new Vector(1, 0);
                case MoveDirection.Down:
                    return new Vector(0, 1);
                case MoveDirection.Left:
                    return new Vector(-1, 0);
                default:
                    return new Vector(0, 0);
            }
        }

        private class Traversals
        {
            // Build a list of positions to traverse in the right order
            public Traversals(int size, Vector vector)
            {
                var xs = new List<int>();
                var ys = new List<int>();

                for (var pos = 0; pos < size; pos++)
                {
                    xs.Add(pos);
                    ys.Add(pos);
                }

                // Always traverse from the farthest cell in the chosen direction

                if (vector.X == 1) xs.Reverse();
                if (vector.Y == 1) ys.Reverse();

                Xs = xs;
                Ys = ys;
            }

            public IEnumerable<int> Xs { get; private set; }
            public IEnumerable<int> Ys { get; private set; }
        }

        private Positions FindFarthestPosition(CellPosition cell, Vector vector)
        {
            CellPosition previous;

            // Progress towards the vector direction until an obstacle is found
            do
            {
                previous = cell;

                cell = new CellPosition(previous.X + vector.X, previous.Y + vector.Y);
            } while (_grid.WithinBounds(cell) &&
                     _grid.CellAvailable(cell));

            return new Positions(
                previous,
                cell // Used to check if a merge is required
                );
        }

        private class Positions
        {
            public Positions(CellPosition farthest, CellPosition next)
            {
                Farthest = farthest;
                Next = next;
            }
            public CellPosition Farthest { get; private set; }
            public CellPosition Next { get; private set; }
        }

        private struct Vector
        {
            public Vector(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; private set; }
            public int Y { get; private set; }

            public override string ToString()
            {
                return string.Format("[{0},{1}]", X, Y);
            }
        }

        #endregion

        #endregion

        #region Setup
        // Set up the game
        public void Setup()
        {
            var previousState = GetAsync(() => _localStorageManager.GetGameState());

            // Reload the game from a previous game if present
            if (null != previousState)
            {
                _grid = new GameGrid(previousState.Grid.Size,
                                            previousState.Grid); // Reload grid

                BestScore = GetAsync(() => _localStorageManager.GetBestScore());
                Score = previousState.Score;
                Over = previousState.Over;
                Won = previousState.Won;
                KeepPlaying = previousState.KeepPlaying;
            }
            else {
                _grid = new GameGrid(_size);
                Score = 0;
                Over = false;
                Won = false;
                KeepPlaying = false;

                // Add the initial tiles
                AddStartTiles();
            }

            // Update the actuator
            Actuate();
        }

        private T GetAsync<T>(Func<Task<T>> func)
        {
            return Task.Run(async () => await func()).Result;
        }
        // Set up the initial tiles to start the game with
        private void AddStartTiles()
        {
            for (var i = 0; i < _startTiles; i++)
            {
                AddRandomTile();
            }
        }

        // Sends the updated grid to the actuator
        private void Actuate()
        {
            var savedScore = GetAsync(() => _localStorageManager.GetBestScore());
            if (savedScore < Score)
            {
                _localStorageManager.SetBestScore(Score);
                BestScore = Score;
            }

            // Clear the state when the game is over (game over only, not win)
            if (Over)
                _localStorageManager.ClearGameState();
            else {
                _localStorageManager.SetGameState(this);
            }

            _inputManager.Actuate(this);
        }

        private int CalculateWiningTileValue(int size)
        {
            switch (size)
            {
                case 4:
                    return 2048;
                case 5:
                    return 8192;
                case 6:
                    return 32768;
                case 7:
                    return 131072;
                case 8:
                    return 524288;
                default:
                    return 2048;
            }
        }

        #endregion

        #endregion
    }
}
