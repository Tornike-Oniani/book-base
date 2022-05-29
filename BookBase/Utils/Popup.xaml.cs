using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookBase.Utils
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : UserControl
    {
        public static DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(Popup));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static DependencyProperty CloseProperty = DependencyProperty.Register(nameof(Close), typeof(ICommand), typeof(Popup));
        public ICommand Close {
            get { return (ICommand)GetValue(CloseProperty); }
            set { SetValue(CloseProperty, value); } 
        }

        public Popup()
        {
            InitializeComponent();
        }
    }
}
