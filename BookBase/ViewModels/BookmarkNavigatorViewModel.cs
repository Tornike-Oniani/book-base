using BookBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookBase.ViewModels
{
    class BookmarkNavigatorViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel currentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged("currentViewModel"); }
        }

        public BookmarkNavigatorViewModel(Action<bool> working): base(working)
        {
            currentViewModel = new BookmarkListViewModel(navigate, working);
        }

        private void navigate(BaseViewModel viewModel)
        {
            currentViewModel = viewModel;
        }
    }
}
