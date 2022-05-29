using BookBase.Models;
using BookBase.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BookBase.Views
{
    /// <summary>
    /// Interaction logic for booksView.xaml
    /// </summary>
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox txbTitle = sender as TextBox;

            // Regex to switch multiple spaces into one (Restricts user to enter more than one space in Title textboxes)
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            Regex unusualCharacters = new Regex("^[A-Za-z0-9 .,'()+/_?:\"\\&*%$#@<>{}!=;-]+$");

            txbTitle.Text = txbTitle.Text.Trim();
            txbTitle.Text = regex.Replace(txbTitle.Text, " ");

            if (!string.IsNullOrEmpty(txbTitle.Text) & !unusualCharacters.IsMatch(txbTitle.Text))
            {
                MessageBox.Show("This input contains unusual characters, please retype it manually. (Don't copy & paste!)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txbTitle.Text = null;
                return;
            }

            Book book;
            if (new BookRepo().checkBookByName(txbTitle.Text, out book))
            {
                MessageBoxResult dialogResult = MessageBox.Show($"A book with the given name already exists in database, do you want to see the file?", 
                                                                "Duplicate", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // User clicked yes
                    // Set the file path with filename and FolderPath static attribute
                    string full_path = Path.Combine(Environment.CurrentDirectory, "Files", book.file + book.extension);

                    // Open given file with default program
                    Process.Start(full_path);
                }
            }
        }
    }
}
