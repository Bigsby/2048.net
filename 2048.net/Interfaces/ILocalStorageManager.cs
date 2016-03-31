using System.Threading.Tasks;

namespace DCCC.Interfaces
{
    public interface ILocalStorageManager
    {
        Task<uint> GetBestScore();
        Task SetBestScore(uint score);
        Task<IGameState> GetGameState();
        Task SetGameState(IGameState gameState);
        Task ClearGameState();
        Task ClearAllData();
    }

    public class DefaultLocalStorageManager : ILocalStorageManager
    {
        public Task ClearAllData()
        {
            return Task.FromResult(0);
        }

        public Task ClearGameState()
        {
            return Task.FromResult(0);
        }

        public Task<uint> GetBestScore()
        {
            return Task.FromResult(0U);
        }

        public Task<IGameState> GetGameState()
        {
            return Task.FromResult((IGameState)null);
        }

        public Task SetBestScore(uint score)
        {
            return Task.FromResult(0);
        }

        public Task SetGameState(IGameState gameState)
        {
            return Task.FromResult(0);
        }
    }
}