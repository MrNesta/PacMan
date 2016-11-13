using PacMan.View;
using PacMan.ViewModel;

namespace PacMan.Infrastructure
{
    public class RecordsTableDialogService : IDialogService
    {
        private RecordsTableWindow _recordsTable;
        private DialogViewModelBase _dataContext;
        private string _message;

        public RecordsTableDialogService(DialogViewModelBase dataContext)
        {
            _message = "RecordsTable";
            _dataContext = dataContext;
        }

        public string Message { get { return _message; } }

        public bool Open()
        {
            _recordsTable = new RecordsTableWindow
            {
                DataContext = _dataContext
            };

            return _recordsTable.ShowDialog().Value;
        }
    }
}
