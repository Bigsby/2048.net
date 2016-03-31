using System;
using System.Collections.Generic;

namespace DCCC.Interfaces
{
    public interface IUIManager
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
