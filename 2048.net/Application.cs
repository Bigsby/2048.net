using DCCC.Interfaces;

namespace DCCC
{
    public class GameApplication
    {
        private IInputManager _inputManager;
        private ILocalStorageManager _localStorageManager;
        public GameApplication(IInputManager inputManager, ILocalStorageManager localStorageManager = null)
        {
            _inputManager = inputManager;
            _localStorageManager = localStorageManager ?? new DefaultLocalStorageManager();
            Start(4);
        }

        private void Start(int size) {
            var gameManger = new GameManager(4, _inputManager, _localStorageManager);
        }
    }
}
