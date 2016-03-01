using DCCC.Interfaces;

namespace DCCC
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
