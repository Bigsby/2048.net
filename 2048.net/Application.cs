using DCCC.Interfaces;

namespace DCCC
{
    public class Application
    {
        private IInputManager _inputManager;
        private ILocalStorageManager _localStorageManager;
        public Application(IInputManager inputManager, ILocalStorageManager localStorageManager = null)
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
