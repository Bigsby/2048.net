using System.Linq;
using Xamarin.Forms;

namespace DCCC.XF
{
    //Base 2C4870
    //Blues
    //> 090D13
    //> 1C2B42
    //> 2C4770
    //> 39629E
    //> 4A87E1

    //Purples
    //> 0B0A14
    //> 251F45
    //> 3C3176
    //> 5040A6
    //> 6751E4

    //Yellows
    //> 1E170C
    //> 644C25
    //> AAB039
    //> F0B248
    //> FFBA43
    internal static class GameColors
    {
        private readonly static Color[] _tileBackgroundColors = new[] {
            Color.Transparent,
            Color.FromHex("eee4da"), //2
            Color.FromHex("ede0c8"),
            Color.FromHex("f2b179"), //8
            Color.FromHex("f59563"),
            Color.FromHex("f67c5f"), //32
            Color.FromHex("f65e3b"),
            Color.FromHex("edcf72"), //128
            Color.FromHex("edcc61"),
            Color.FromHex("edc850"), //512
            Color.FromHex("edc53f"),
            Color.FromHex("edc22e"), //2048
            Color.FromHex("3c3a32")
        };

		private readonly static Color _smallColor = Color.FromHex("776e65");
        private readonly static Color _bigColoer = Color.FromHex("f9f6f2");


        public static Color GetTileBackgroundColor(int value)
        {
            return GetColor(_tileBackgroundColors, value);
        }

        public static Color GetTileColor(int value)
        {
            return value > 2 ? _bigColoer : _smallColor;
        }

        private static Color GetColor(Color[] colors, int value)
        {
            return value < colors.Length ?
                colors[value]
                :
                colors.Last();
        }

        public static Color ScoreCaptionColor = Color.FromHex("39629E");
        public static Color ScoreColor = Color.FromHex("4A87E1");
    }
}
