using System;

namespace DCCC.Interfaces
{
    public interface IInputManager
    {
        void OnMove(Action<MoveDirection> callback);
        void OnRestart(Action callback);
        void OnKeepPlaying(Action callback);
        void Actuate(IGameState gameState);
        void ContinueGame();

    }

    public enum MoveDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
