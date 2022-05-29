using BookBase.ViewModels;
using BookBase.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookBase.Utils
{
    class UpdateViewCommand : ICommand
    {
        private Action<BaseViewModel> _navigate;
        private Action<bool> working;

        public event EventHandler CanExecuteChanged;

        public UpdateViewCommand(Action<BaseViewModel> navigate, Action<bool> working)
        {
            this._navigate = navigate;
            this.working = working;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch ((ViewType)parameter)
            {
                case ViewType.Books:
                    _navigate(new BooksViewModel(working));
                    break;
                case ViewType.BookmarkNavigator:
                    _navigate(new BookmarkNavigatorViewModel(working));
                    break;
            }
        }
    }
}
