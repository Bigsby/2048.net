using Xamarin.Forms;

namespace DCCC.XF.GameControls
{
    public class GameHeader : Grid
    {
        private readonly Label _highScoreLabel;
        private readonly Label _currentScoreLabel;
        private readonly double _fontSize;

        public GameHeader(double fontSize)
        {
            _fontSize = fontSize;
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());

            var highScoreCaption = GetCenteredLabel();
            highScoreCaption.Text = "Best";
            Children.Add(highScoreCaption);

            var currentScoreCaption = GetCenteredLabel();
            currentScoreCaption.Text = "Score";
            SetColumn(currentScoreCaption, 1);
            Children.Add(currentScoreCaption);

            _highScoreLabel = GetScoreLabel();
            _currentScoreLabel = GetScoreLabel();

            SetColumn(_currentScoreLabel, 1);

            Children.Add(_highScoreLabel);
            Children.Add(_currentScoreLabel);
        }

        internal void Update(uint highScore, uint currentScore)
        {
            _highScoreLabel.Text = highScore.ToString();
            _currentScoreLabel.Text = currentScore.ToString();
        }

        private Label GetScoreLabel()
        {
            var result = GetCenteredLabel();
            result.FontSize = _fontSize;
            result.TextColor = GameColors.ScoreColor;
            result.FontAttributes = FontAttributes.Bold;
            SetRow(result, 1);
            return result;
        }

        private Label GetCenteredLabel()
        {
            return new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = GameColors.ScoreCaptionColor,
                FontSize = _fontSize * .75
            };
        }
    }
}
