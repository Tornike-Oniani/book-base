using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BookBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _selectedTheme;

        public List<string> themes { get; set; }
        public string selectedTheme
        {
            get { return _selectedTheme; }
            set 
            { 
                _selectedTheme = value; 
                OnPropertyChanged("selectedTheme");
                changeTheme(value.ToString());
                Properties.Settings.Default.theme = value;
                Properties.Settings.Default.Save();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            themes = new List<string>()
            {
                "Light",
                "Dark"
            };
            string chosenTheme = Properties.Settings.Default.theme;
            int index = themes.FindIndex((cur) => cur == chosenTheme);
            selectedTheme = themes[index];
        }

        private void changeTheme(string theme)
        {
            string name = theme == "Light" ? "" : "Dark";

            // Background and border colors
            Color color = getColor($"BackgroundColor{name}");
            ((SolidColorBrush)Application.Current.Resources["BackgroundColorBrush"]).Color = color;
            color = getColor($"BackgroundLineColor{name}");
            ((SolidColorBrush)Application.Current.Resources["BackgroundLineColorBrush"]).Color = color;
            color = getColor($"BorderColor{name}");
            ((SolidColorBrush)Application.Current.Resources["BorderColorBrush"]).Color = color;
        }

        private Color getColor(string name)
        {
            return (Color)Application.Current.Resources[name];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
