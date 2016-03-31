using DCCC.Interfaces;
using System.Threading.Tasks;

namespace DCCC
{
    public class GameApplication
    {
        private readonly IUIManager _inputManager;
        private readonly ILocalStorageManager _localStorageManager;
        private GameManager _gameManager;

        public GameApplication(IUIManager inputManager, ILocalStorageManager localStorageManager = null)
        {
            _inputManager = inputManager;
            _localStorageManager = localStorageManager ?? new DefaultLocalStorageManager();
            Start(4);
        }

        private void Start(int size)
        {
            _gameManager = new GameManager(4, _inputManager, _localStorageManager);
        }

        public void SaveState()
        {
            Task.Run(async () => await _localStorageManager.SetGameState(_gameManager));
        }

        public void Resume()
        {
            _gameManager.Setup();   
        }
    }
}
