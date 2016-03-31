using DCCC.Interfaces;
using System;
using System.Collections.Generic;

namespace DCCC.BaseImplementations
{
    public abstract class BaseUIManager : IUIManager
    {
        protected IGamePage _gamePage;
        private Action<MoveDirection> _moveHandler;
        private Action _restartHanlder;
        private Action _keepPlayingHandler;

        protected BaseUIManager(IGamePage gamePage)
        {
            _gamePage = gamePage;
            _gamePage.Ready += (s, e) => Ready.Invoke(this, EventArgs.Empty);
            _gamePage.Moved += (s, e) => _moveHandler(e.Direction);
        }

        public event EventHandler Ready;

        public void Actuate(IGameState gameState)
        {
            _gamePage.Update(gameState);

            if (gameState.Won && !gameState.KeepPlaying)
                _keepPlayingHandler();



            if (gameState.Over)
            {
                Action cleanup = null;
                Action restart = () =>
                {
                    if (null != cleanup) cleanup();
                    _restartHanlder();
                };

                var gameOverOptions = new GameOption[] {
                    new GameOption("Restart", restart)
                };

                cleanup = ShowOptions(false, "Game Over!", gameOverOptions);
            }
        }

        public void ContinueGame()
        {
            //throw new NotImplementedException();
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

        protected double FontSize { get { return _gamePage.CalculatedFontSize; } }

        protected abstract void ConfirmKeepGoing(Action<bool> handler);

        protected abstract Action ShowOptions(bool hideScore, string title, IEnumerable<GameOption> options);
    }
}
