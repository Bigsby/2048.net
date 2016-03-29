using DCCC.Interfaces;
using System;
using System.Collections.Generic;

namespace DCCC.XF
{
    public class XFInputManager : BaseInputManager, IInputManager
    {
        private readonly GamePage _gamePage;
        public XFInputManager(GamePage gamePage)
            : base(gamePage)
        {
            _gamePage = gamePage;
        }

        protected override void ConfirmKeepGoing(Action<bool> handler)
        {
            _gamePage.ConfirmKeepGoing(handler);
        }

        protected override void ShowOptions(IEnumerable<Interfaces.GameOption> options)
        {
            throw new NotImplementedException();
        }
    }
}
