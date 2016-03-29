using System;
using Xamarin.Forms;

namespace DCCC.XF
{
    public class GameGrid : Grid
    {
        private readonly int _size;
        private GameCell[,] _cells;
        private double _childDimension;
        private double _spacing;

        public GameGrid(double dimension, int size)
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

            foreach (var tile in tiles)
            {
                if (tile == null) continue;

                var cell = _cells[tile.Position.X, tile.Position.Y];
                cell.Value = tile.Value;
                if (tile.Value == 0) return;
                if (tile.IsNew)
                    AnimateNew(cell);
                else
                {
                    if (null != tile.MergedFrom)
                        AnimateCell(cell, tile.MergedFrom.Previous.Position, tile.MergedFrom.Next.Position);

                    if (!tile.Position.IsEqual(tile.PreviousPosition))
                        AnimateCell(cell, tile.PreviousPosition, tile.Position);
                }
            }
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

            cell.Animate("tileMove", new Animation(animationFunction, start, 0));

        }

        private double CalculateDistance(int origin, int target)
        {
            var difference = Math.Abs(target - origin);
            var distance = difference * _childDimension + ((difference + 1) * _spacing);
            return target > origin ? distance : -distance;
        }

        private void AnimateNew(GameCell cell)
        {
            cell.Animate("newTile", new Animation((double scale) => cell.Scale = scale));
        }
    }
}
