using GalaSoft.MvvmLight;

namespace PacMan.ViewModel
{
    public abstract class DialogViewModelBase: ViewModelBase
    {
        private bool? _dialogResult;

        public bool? DialogResult
        {
            get { return _dialogResult; }
            protected set
            {
                _dialogResult = value;
                RaisePropertyChanged("DialogResult");
            }
        }
    }
}
