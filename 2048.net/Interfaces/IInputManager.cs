using System;
using System.Collections.Generic;

namespace DCCC.Interfaces
{
    public interface IInputManager
    {
        void OnMove(Action<MoveDirection> callback);
        void OnRestart(Action callback);
        void OnKeepPlaying(Action callback);
        void Actuate(IGameState gameState);
        void ContinueGame();
        event EventHandler Ready;
    }

    public interface IGamePage
    {
        void Update(IGameState gaemState);
        double CalculatedFontSize { get; }
        event EventHandler Ready;
        event EventHandler<MoveEventArgs> Moved;
    }

    public abstract class BaseInputManager : IInputManager
    {
        protected IGamePage _gamePage;
        private Action<MoveDirection> _moveHandler;
        private Action _restartHanlder;
        private Action _keepPlayingHandler;

        protected BaseInputManager(IGamePage gamePage)
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
            //ConfirmKeepGoing(result => { if (result) _keepPlayingHandler(); });
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

    public class GameOption
    {
        public GameOption(string caption, Action callback)
        {
            Caption = caption;
            Callback = callback;
        }
        public string Caption { get; private set; }
        public Action Callback { get; private set; }
    }

    public enum MoveDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public class MoveEventArgs : EventArgs
    {
        public MoveEventArgs(MoveDirection direction)
        {
            Direction = direction;
        }
        public MoveDirection Direction { get; private set; }
    }
}
