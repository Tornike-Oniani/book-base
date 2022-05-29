using BookBase.Models;
using BookBase.Repositories;
using BookBase.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookBase.ViewModels
{
    class BookmarkViewModel : BaseViewModel
    {
        private Action<BaseViewModel> navigate;
        private List<Book> _books;

        public Bookmark bookmark { get; set; }
        public List<Book> books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged("books"); }
        }

        public ICommand goToBookmarksCommand { get; set; }
        public ICommand removeBookCommand { get; set; }
        public ICommand openFileCommand { get; set; }

        public BookmarkViewModel(Bookmark bookmark, Action<BaseViewModel> navigate, Action<bool> working) : base(working)
        {
            this.navigate = navigate;
            this.bookmark = bookmark;
            goToBookmarksCommand = new RelayCommand(goToBookmarks);
            removeBookCommand = new RelayCommand(removeBook);
            openFileCommand = new RelayCommand(openFile);
            LoadFirstBooks();
        }

        public void goToBookmarks(object input = null)
        {
            navigate.Invoke(new BookmarkListViewModel(navigate, working));
        }
        public async void removeBook(object input)
        {
            working(true);
            await Task.Run(() =>
            {
                Book book = input as Book;
                MessageBoxResult dialogResult = MessageBox.Show($"Are you sure you want to remove {book.name} from {bookmark.name}?", "Delete book", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    working(true);
                    new BookmarkRepo().removeBook(bookmark, book);
                    LoadBooks();
                    working(false);
                }
            });
            working(false);
        }
        public void openFile(object input)
        {
            if (input == null) return;

            Book book = input as Book;
            Process.Start(Path.Combine(Environment.CurrentDirectory, "Files", book.file + book.extension));
        }

        private void LoadBooks()
        {
            books = new BookmarkRepo().getBooks(bookmark.name);
        }

        private async void LoadFirstBooks()
        {
            working(true);
            await Task.Run(() =>
            {
                LoadBooks();
            });
            working(false);
        }
    }
}
