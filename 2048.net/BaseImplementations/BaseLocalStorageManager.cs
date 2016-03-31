using DCCC.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace DCCC.BaseImplementations
{
    public abstract class BaseLocalStorageManager : ILocalStorageManager
    {
        private const string _gameStateFileName = "gameStage.json";
        private const string _scoreFileName = "score.json";

        public async Task ClearAllData()
        {
            await ClearGameState();
            await DeleteFile(_scoreFileName);
        }

        public async Task ClearGameState()
        {
            await DeleteFile(_gameStateFileName);
        }

        public async Task<uint> GetBestScore()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Read Score!");
                return uint.Parse(await ReadFile(_scoreFileName).ConfigureAwait(false));
            }
            catch
            {
                return 0U;
            }
        }

        public async Task<IGameState> GetGameState()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Read GameState!");
                return StoredGameState.Build(JsonConvert.DeserializeObject<SerializableGameState>(await ReadFile(_gameStateFileName).ConfigureAwait(false)));
            }
            catch
            {
                return null;
            }
        }

        public async Task SetBestScore(uint score)
        {
            try
            {
                await WriteToFile(_scoreFileName, score.ToString()).ConfigureAwait(false);
                System.Diagnostics.Debug.WriteLine("Score Saved!");
            }
            catch
            {
                
            }
        }

        public async Task SetGameState(IGameState gameState)
        {
            await WriteToFile(_gameStateFileName, JsonConvert.SerializeObject(SerializableGameState.Build(gameState))).ConfigureAwait(false);
            System.Diagnostics.Debug.WriteLine("GameState Saved!");
        }

        protected abstract Task DeleteFile(string fileName);
        protected abstract Task<string> ReadFile(string fileName);
        protected abstract Task WriteToFile(string fileName, string content);
    }

    public class StoredGameState : IGameState
    {
        public GameGrid Grid { get; set; }
        public bool KeepPlaying { get; set; }
        public bool Over { get; set; }
        public uint Score { get; set; }
        public uint BestScore { get; set; }
        public bool Won { get; set; }

        public static StoredGameState Build(SerializableGameState storedState)
        {
            return new StoredGameState
            {
                Grid = new GameGrid(storedState.Size, storedState.Tiles),
                KeepPlaying = storedState.KeepPlaying,
                Over = storedState.Over,
                Score = storedState.Score,
                Won = storedState.Won
            };
        }
    }

    public class SerializableGameState
    {
        public GameTile[,] Tiles { get; set; }
        public int Size { get; set; }
        public bool KeepPlaying { get; set; }
        public bool Over { get; set; }
        public uint Score { get; set; }
        public bool Won { get; set; }

        public static SerializableGameState Build(IGameState state)
        {
            return new SerializableGameState
            {
                Tiles = state.Grid.Cells,
                KeepPlaying = state.KeepPlaying,
                Over = state.Over,
                Score = state.Score,
                Size = state.Grid.Size,
                Won = state.Won,
            };
        }
    }
}
