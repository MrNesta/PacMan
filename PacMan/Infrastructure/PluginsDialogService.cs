using System;
using GalaSoft.MvvmLight.Messaging;
using PacMan.View;
using PacMan.ViewModel;

namespace PacMan.Infrastructure
{
    public class PluginsDialogService : IDialogService
    {
        private PluginWindow _window;
        string _message;
        private DialogViewModelBase _dataContext;

        public PluginsDialogService(DialogViewModelBase dataContext)
        {
            _dataContext = dataContext;
            Messenger.Default.Register(this, new Action<Type>(ProcessMessage));
        }

        private void ProcessMessage(Type type)
        {
            if (type != null)
            {
                _message = type.Assembly.Location;
            }
            else _message = null;
        }

        public string Message { get { return _message; } }

        public bool Open()
        {
            _window = new PluginWindow();
            _window.DataContext = _dataContext;
            return _window.ShowDialog().Value;
        }
    }
}
