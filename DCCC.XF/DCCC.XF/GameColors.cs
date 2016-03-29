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
        private static Color[] _tileColors = new[] {
            Color.FromHex("4A87E1"),
            Color.FromHex("FFBA43"),
            Color.FromHex("39629E"),
            Color.FromHex("F0B248"),
            Color.FromHex("2C4770"),
            Color.FromHex("AAB039"),
            Color.FromHex("1C2B42"),
            Color.FromHex("644C25"),
            Color.FromHex("090D13"),
            Color.FromHex("1E170C")
        };

        public static Color GetTileColor(int value)
        {
            if (value < _tileColors.Length)
                return _tileColors[value];
            return _tileColors.Last();
        }

        public static Color ScoreCaptionColor = Color.FromHex("3C3176");
        public static Color ScoreColor = Color.FromHex("6751E4");
    }
}
