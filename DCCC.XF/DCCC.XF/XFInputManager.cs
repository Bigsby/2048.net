using DCCC.Interfaces;
using System;
using System.Collections.Generic;

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

        protected override void ShowOptions(IEnumerable<Interfaces.GameOption> options)
        {
            throw new NotImplementedException();
        }
    }
}
