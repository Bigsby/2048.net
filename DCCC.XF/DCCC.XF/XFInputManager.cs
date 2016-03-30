using DCCC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DCCC.XF
{
    public class XFInputManager : BaseInputManager, IInputManager
    {
        private readonly GamePage _xfGamePage;
        public XFInputManager(GamePage gamePage)
            : base(gamePage)
        {
            _xfGamePage = gamePage;
        }

        protected override void ConfirmKeepGoing(Action<bool> handler)
        {
            _xfGamePage.ConfirmKeepGoing(handler);
        }

        protected override Action ShowOptions(bool hideScore, string title, IEnumerable<GameOption> options)
        {
            return _xfGamePage.ShowOptions(hideScore, new GameOptions(FontSize, title, options.ToArray()));
        }
    }
}
