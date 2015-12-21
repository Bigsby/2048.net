using _2048.net.Interfaces;

namespace _2048.net
{
    public class Application
    {
        private IInputManager _inputManager;
        private ILocalStorageManager _localStorageManager;
        public Application(IInputManager inputManager, ILocalStorageManager localStorageManager)
        {
            _inputManager = inputManager;
            _localStorageManager = localStorageManager;
        }

        private void Start(int size) {

        }
    }
}
