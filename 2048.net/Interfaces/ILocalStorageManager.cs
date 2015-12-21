namespace _2048.net.Interfaces
{
    public interface ILocalStorageManager
    {
        uint GetBestScore();
        void SetBestScore(uint score);
        IGameState GetGameState();
        void SetGameState(IGameState gameState);
        void ClearGameState();
        void ClearAllData();
    }

    public interface IGameState
    {
        Grid Grid { get; set; }
        uint Score { get; set; }
        bool Over { get; set; }
        bool Won { get; set; }
        bool KeepPlaying { get; set; }
    }
}
