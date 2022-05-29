using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBase.ViewModels
{
    class BaseViewModel : INotifyPropertyChanged
    {
        protected Action<bool> working;

        public BaseViewModel(Action<bool> working)
        {
            this.working = working;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
