using BookBase.Models;
using BookBase.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookBase.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private BaseViewModel _selectedViewModel;
        private bool _isBusy;

        public BaseViewModel selectedViewModel
        {
            get { return _selectedViewModel; }
            set { _selectedViewModel = value; OnPropertyChanged("selectedViewModel"); }
        }
        public bool isBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged("isBusy"); }
        }

        public ICommand updateViewCommand { get; set; }

        public MainWindowViewModel()
        {
            this.updateViewCommand = new UpdateViewCommand(navigate, working);
            navigate(new BooksViewModel(working));
            isBusy = false;

            if (Application.Current != null)
            {
                Application.Current.DispatcherUnhandledException += (s, a) =>
                {
                    working(false);

                    // 1. Handle unique constraint
                    if (a.Exception.Message.Contains("UNIQUE"))
                    {
                        string name = extractPropertyFromSqliteError(a.Exception.Message);
                        MessageBox.Show($"{name} with that name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        a.Handled = true;
                        return;
                    }

                    // 1. Handle not null constraint
                    if (a.Exception.Message.Contains("NOT NULL"))
                    {
                        string name = extractPropertyFromSqliteError(a.Exception.Message);
                        MessageBox.Show($"{name} can not be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        a.Handled = true;
                        return;
                    }

                    // 2. Generic unhandled exceptions
                    MessageBox.Show($"{a.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    a.Handled = true;
                };
            }
        }

        public void navigate(BaseViewModel viewModel)
        {
            this.selectedViewModel = viewModel;
        }
        public void working(bool status)
        {
            isBusy = status;
        }

        private string extractPropertyFromSqliteError(string message)
        {
            string[] tableProperty = message.Split(new string[] { ": " }, StringSplitOptions.None);
            string[] name = tableProperty[1].Split('.');
            return name[0];
        }
    }
}
