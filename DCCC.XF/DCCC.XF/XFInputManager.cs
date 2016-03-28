using DCCC.Interfaces;
using System;

namespace DCCC.XF
{
    public class XFInputManager : IInputManager
    {
        private readonly GamePage _gamePage;
        public XFInputManager(GamePage gamePage)
        {
            _gamePage = gamePage;
            _gamePage.Moved += (s, e) => _moveHandler(e.Direction);
            _gamePage.Ready += (s, e) => Ready?.Invoke(this, EventArgs.Empty);
        }

        private Action<MoveDirection> _moveHandler;
        private Action _restartHanlder;
        private Action _keepPlayingHandler;

        public event EventHandler Ready;

        public void Actuate(IGameState gameState)
        {
            if (gameState.Won)
                gameState.KeepPlaying = true;

            _gamePage.Update(gameState);
        }

        public void ContinueGame()
        {
            throw new NotImplementedException();
        }

        public void OnKeepPlaying(Action callback)
        {
            _keepPlayingHandler = callback;
        }

        public void OnMove(Action<MoveDirection> callback)
        {
            _moveHandler = callback;
        }

        public void OnRestart(Action callback)
        {
            _restartHanlder = callback;
        }
    }
}
