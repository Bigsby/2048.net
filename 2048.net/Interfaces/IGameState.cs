namespace DCCC.Interfaces
{

    public interface IGameState
    {
        GameGrid Grid { get; set; }
        uint Score { get; set; }
        bool Over { get; set; }
        bool Won { get; set; }
        bool KeepPlaying { get; set; }
    }
}
