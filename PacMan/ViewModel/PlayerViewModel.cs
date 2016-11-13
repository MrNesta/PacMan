using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace PacMan.ViewModel
{
    public class PlayerViewModel: DialogViewModelBase
    {        
        private string _currentPlayer;

        public string CurrentPlayer
        {
            get
            {
                if (_currentPlayer == null)
                    _currentPlayer = "anonymous"; 
                return _currentPlayer;
            }

            set
            {
                _currentPlayer = value;
                RaisePropertyChanged("CurrentPlayer");
            }
        }

        private RelayCommand _addPlayerCommand;

        public ICommand AddPlayer
        {
            get
            {
                if (_addPlayerCommand == null)
                    _addPlayerCommand = new RelayCommand(
                        ExecuteAddPlayerCommand, 
                        CanExecuteAddPlayerCommand);
                return _addPlayerCommand;
            }
        }

        public void ExecuteAddPlayerCommand()
        {           
            Messenger.Default.Send(_currentPlayer);
            CurrentPlayer = null;
            DialogResult = true;
        }

        public bool CanExecuteAddPlayerCommand()
        {
            if (string.IsNullOrEmpty(CurrentPlayer))
                return false;
            return true;
        }
    }
}
