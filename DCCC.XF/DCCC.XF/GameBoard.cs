using System;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameBoard : Grid
    {
        private readonly int _size;
        private GameCell[,] _cells;
        private double _childDimension;
        private double _spacing;
        private const uint _animationLength = 150;

        public GameBoard(double dimension, int size)
        {
            _size = size;
            BackgroundColor = Color.FromHex("101010");
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

            for (int xIndex = 0; xIndex < _size; xIndex++)
                for (int yIndex = 0; yIndex < _size; yIndex++)
                {
                    var cell = new GameCell(_childDimension);
                    Children.Add(cell);
                    SetRow(cell, yIndex);
                    SetColumn(cell, xIndex);
                    _cells[xIndex, yIndex] = cell;
                }
        }

        public void Update(GameTile[,] tiles)
        {
            foreach (var cell in _cells)
                cell.Value = 0;

            GameTile newTile = null;
            GameCell newCell = null;

            foreach (var tile in tiles)
            {
                if (tile == null) continue;

                var cell = _cells[tile.Position.X, tile.Position.Y];

                if (tile.Value == 0) return;
                if (tile.IsNew)
                {
                    newCell = cell;
                    newTile = tile;
                }
                else if (null != tile.MergedFrom)
                    AnimateMerge(cell, tile);
                else if (!tile.Position.IsEqual(tile.PreviousPosition))
                {
                    cell.Value = tile.Value;
                    AnimateCell(cell, tile.PreviousPosition, tile.Position);
                }
            }

            if (null != newTile && null != newCell)
            {
                newCell.Value = newTile.Value;
                AnimateNew(newCell);
            }

        }

        private void AnimateMerge(GameCell targetCell, GameTile tile)
        {
            var sourceCell = _cells[tile.MergedFrom.Previous.X, tile.MergedFrom.Previous.Y];
            sourceCell.Value = targetCell.Value = tile.Value / 2;

            AnimateCell(sourceCell, tile.MergedFrom.Previous, tile.MergedFrom.Next);
            sourceCell.Value = 0;
            targetCell.Value = tile.Value;
        }

        private void AnimateCell(GameCell cell, CellPosition origin, CellPosition target)
        {
            Action<double> animationFunction = origin.X == target.X
                ?
                new Action<double>(translation => cell.TranslationY = translation)
                :
                new Action<double>(translation => cell.TranslationX = translation);

            var start = origin.X == target.X ?
                CalculateDistance(origin.Y, target.Y)
                :
                CalculateDistance(origin.X, target.X);

            cell.Animate("tileMove", new Animation(animationFunction, start, 0), length: _animationLength, finished: (d, b) =>
             {
                 cell.TranslationX = 0;
                 cell.TranslationY = 0;
             });

        }

        private double CalculateDistance(int origin, int target)
        {
            var difference = Math.Abs(target - origin);
            var distance = difference * _childDimension + ((difference + 1) * _spacing);
            return target > origin ? -distance : distance;
        }

        private void AnimateNew(GameCell cell)
        {
            cell.Animate("newTile", new Animation((double scale) => cell.Scale = scale, .1, 1), length: _animationLength, finished: (d, b) => cell.Scale = 1);
        }
    }
}
