using System;
using Xamarin.Forms;

namespace DCCC.XF.GameControls
{
    public class GameBoard : Grid
    {
        private readonly int _size;
        private GameCell[,] _cells;
        private double _childDimension;
        private double _spacing;
        private const uint _moveAnimationLength = 50;
        private const uint _newAnimationLength = 100;


        public GameBoard(double dimension, int size)
        {
            _size = size;
        //    BackgroundColor = Color.FromHex("142F54");
            _spacing = dimension * .01;
            Padding = RowSpacing = ColumnSpacing = _spacing;
            WidthRequest = HeightRequest = dimension;

            _cells = new GameCell[_size, _size];

            for (int index = 0; index < _size; index++)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            _childDimension = (dimension - (_spacing * (size + 1))) / size;

            BuildGrid((x, y) => BuildBackgroundGrid(_childDimension));

            BuildGrid((x, y) =>
               {
                   var cell = new GameCell(_childDimension);
                   _cells[x, y] = cell;
                   return cell;
               });
        }

        private void BuildGrid(Func<int, int, Grid> itemConstructor)
        {
            for (int xIndex = 0; xIndex < _size; xIndex++)
                for (int yIndex = 0; yIndex < _size; yIndex++)
                {
                    var cell = itemConstructor(xIndex, yIndex);
                    Children.Add(cell);
                    SetRow(cell, yIndex);
                    SetColumn(cell, xIndex);
                }
        }

        private Grid BuildBackgroundGrid(double size)
        {
            return new Grid
            {
                BackgroundColor = Color.FromHex("142F54"),
                HeightRequest = size,
                WidthRequest = size
            };
        }

        private GameTile[] _newTiles = new GameTile[2];

        public void Update(GameTile[,] tiles)
        {
            foreach (var cell in _cells)
                cell.Value = 0;
            _newTiles[0] = null;
            _newTiles[1] = null;

            foreach (var tile in tiles)
            {
                if (tile == null) continue;

                var cell = _cells[tile.Position.X, tile.Position.Y];

                if (tile.Value == 0) return;
                if (tile.IsNew)
                    _newTiles[null == _newTiles[0] ? 0 : 1] = tile;

                else if (null != tile.MergedFrom)
                    AnimateMerge(cell, tile);
                else if (!tile.Position.IsEqual(tile.PreviousPosition))
                {
                    cell.Value = tile.Value;
                    AnimateCell(cell, tile.PreviousPosition, tile.Position);
                }
                else
                    cell.Value = tile.Value;
            }

            foreach (var newTile in _newTiles)
                if (null != newTile)
                {
                    var cell = _cells[newTile.Position.X, newTile.Position.Y];
                    cell.Value = newTile.Value;
                    AnimateNew(cell);
                }

        }

        private void AnimateMerge(GameCell targetCell, GameTile tile)
        {
            //var sourceCell = _cells[tile.MergedFrom.Previous.X, tile.MergedFrom.Previous.Y];
            //sourceCell.Value = targetCell.Value = tile.Value / 2;
            targetCell.Value = tile.Value / 2;
            AnimateCell(targetCell, tile.MergedFrom.Previous, tile.Position, () =>
                targetCell.Value = tile.Value
            );
        }

        private void AnimateCell(GameCell cell, CellPosition origin, CellPosition target, Action finished = null)
        {
            Action<double> animationFunction = origin.X == target.X
                ?
                new Action<double>(translation => cell.TranslationY = translation)
                :
                new Action<double>(translation => cell.TranslationX = translation);

            var distance = origin.X == target.X ?
                CalculateDistance(origin.Y, target.Y)
                :
                CalculateDistance(origin.X, target.X);

            cell.Animate("tileMove", new Animation(animationFunction, distance.Item1, 0), length: distance.Item2 * _moveAnimationLength, finished: (d, b) =>
             {
                 cell.TranslationX = 0;
                 cell.TranslationY = 0;
                 finished?.Invoke();
             });

        }

        private Tuple<double, uint> CalculateDistance(int origin, int target)
        {
            var difference = Math.Abs(target - origin);
            var distance = difference * _childDimension + ((difference + 1) * _spacing);
            return Tuple.Create(target > origin ? -distance : distance, (uint)difference);
        }

        private void AnimateNew(GameCell cell)
        {
            cell.Animate("newTile", new Animation((double scale) => cell.Scale = scale, .1, 1), length: _newAnimationLength, finished: (d, b) => cell.Scale = 1);
        }
    }
}
