using GalaSoft.MvvmLight.Messaging;
using PacMan.View;
using PacMan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.Infrastructure
{
    public class PlayerNameDialogService : IDialogService
    {
        PlayerWindow _pleyerWindow;
        string _message;

        public PlayerNameDialogService(DialogViewModelBase dataContext)
        {
            _pleyerWindow = new PlayerWindow();
            _pleyerWindow.DataContext = dataContext;
            Messenger.Default.Register(this, new Action<string>(ProcessMessage));
        }

        public string Message { get { return _message; }}

        public bool Open()
        {
            return _pleyerWindow.ShowDialog().Value;             
        }

        private void ProcessMessage(string msg)
        {
            if (msg != null)
            {
                _message = msg;
            }
        }
    }
}
