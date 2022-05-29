using BookBase.Models;
using BookBase.Repositories;
using BookBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookBase.ViewModels
{
    class BookmarkListViewModel : BaseViewModel
    {
        private Action<BaseViewModel> navigate;
        private List<Bookmark> _bookmarks;
        private string _bookmarkName;
        private bool _creatingBookmark;
        private bool _bookmarkNameFocus;

        public List<Bookmark> bookmarks
        {
            get { return _bookmarks; }
            set { _bookmarks = value; OnPropertyChanged("bookmarks"); }
        }
        public string bookmarkName
        {
            get { return _bookmarkName; }
            set { _bookmarkName = value; OnPropertyChanged("bookmarkName"); }
        }

        public bool creatingBookmark
        {
            get { return _creatingBookmark; }
            set { _creatingBookmark = value; OnPropertyChanged("creatingBookmark"); }
        }
        public bool bookmarkNameFocus
        {
            get { return _bookmarkNameFocus; }
            set { _bookmarkNameFocus = value; OnPropertyChanged("bookmarkNameFocus"); }
        }



        public ICommand openBookmarkCommand { get; set; }
        public ICommand addBookmarkCommand { get; set; }
        public ICommand createBookmarkCommand { get; set; }
        public ICommand updateBookmarkCommand { get; set; }
        public ICommand deleteBookmarkCommand { get; set; }
        public ICommand enterEditModeCommand { get; set; }
        public ICommand exitEditModeCommand { get; set; }

        public BookmarkListViewModel(Action<BaseViewModel> navigate, Action<bool> working) : base(working)
        {
            this.navigate = navigate;
            openBookmarkCommand = new RelayCommand(openBookmark);
            addBookmarkCommand = new RelayCommand(addBookmark);
            createBookmarkCommand = new RelayCommand(createBookmark);
            updateBookmarkCommand = new RelayCommand(updateBookmark);
            deleteBookmarkCommand = new RelayCommand(deleteBookmark);
            enterEditModeCommand = new RelayCommand(enterEditMode);
            exitEditModeCommand = new RelayCommand(exitEditMode);
            firstLoadBookmarks();
        }

        public void openBookmark(object input)
        {
            navigate.Invoke(new BookmarkViewModel(input as Bookmark, navigate, working));
        }
        public void addBookmark(object input = null)
        {
            creatingBookmark = true;
            bookmarkNameFocus = true;
        }
        public async void createBookmark(object input = null)
        {
            working(true);
            await Task.Run(async () =>
            {
                new BookmarkRepo().create(new Bookmark(bookmarkName));
                creatingBookmark = false;
                bookmarkName = null;
                await loadBookmarks();
                bookmarkNameFocus = false;
            });
            working(false);
        }
        public async void updateBookmark(object input)
        {
            working(true);
            await Task.Run(() =>
            {
                Bookmark bookmark = input as Bookmark;
                new BookmarkRepo().findByIdAndUpadte(bookmark);
                bookmark.editMode = false;
            });
            working(false);
        }
        public async void deleteBookmark(object input)
        {
            working(true);
            await Task.Run(async () =>
            {
                Bookmark bookmark = input as Bookmark;
                MessageBoxResult dialogResult = MessageBox.Show($"Are you sure you want to delete {bookmark.name}?", "Delete book", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    new BookmarkRepo().findByIdAndDelete(bookmark.id);
                    await loadBookmarks();
                }
            });
            working(false);
        }
        public void enterEditMode(object input)
        {
            ((Bookmark)input).editMode = true;
        }
        public void exitEditMode(object input)
        {
            ((Bookmark)input).editMode = false;
        }

        private async Task loadBookmarks()
        {
            await Task.Run(() =>
            {
                List<Bookmark> rawBookmarks = new BookmarkRepo().find(null);

                rawBookmarks.ForEach((Bookmark bookmark) =>
                {
                    bookmark.countBooks();
                });

                bookmarks = rawBookmarks;
            });
        }

        private async void firstLoadBookmarks()
        {
            working(true);
            await loadBookmarks();
            working(false);
        }
        
    }
}
