using BookBase.Models;
using BookBase.Repositories;
using BookBase.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookBase.ViewModels
{
    class BooksViewModel : BaseViewModel, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                if (!createFailed) return null;

                string result = null;

                switch (propertyName)
                {
                    case "name":
                        if (string.IsNullOrWhiteSpace(name))
                            result = "Name can not be empty";
                        break;
                    case "file":
                        if (string.IsNullOrWhiteSpace(file))
                            result = "File can not be empty";
                        break;
                }

                return result;
            }
        }

        private bool _addDataVisibility;
        private List<Book> _books;
        private string _name;
        private int? _year;
        private string _file;
        private string _type;
        private string _nameFilter;
        private string _authorsFilter;
        private int? _yearFilter;
        private string _selectedTypeFilter;
        private bool _bookmarkManagerVisibility;
        private List<Bookmark> _bookmarks;
        private Book _managedBook;
        private string _bookmarkName;
        private bool _nameFocus;
        private bool _bookmarkNameFocus;

        private Regex numbersOnly = new Regex("^[0-9]+$", RegexOptions.None);
        private bool toggled;
        private bool createFailed;

        // Book list
        public bool addDataVisibility
        {
            get { return _addDataVisibility; }
            set { _addDataVisibility = value; OnPropertyChanged("addDataVisibility"); }
        }
        public List<Book> books
        {
            get { return _books; }
            set { _books = value; OnPropertyChanged("books"); }
        }

        // Add popup
        public string name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("name"); }
        }
        public ObservableCollection<string> authors { get; set; }
        public int? year
        {
            get { return _year; }
            set { _year = value; OnPropertyChanged("year"); }
        }
        public string file
        {
            get { return _file; }
            set { _file = value; OnPropertyChanged("file"); }
        }
        public string type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged("type"); }
        }
        public List<string> bookTypes { get; set; }

        public bool nameFocus
        {
            get { return _nameFocus; }
            set { _nameFocus = value; OnPropertyChanged("nameFocus"); }
        }

        // Filter
        public string nameFilter
        {
            get { return _nameFilter; }
            set { _nameFilter = value; OnPropertyChanged("nameFilter"); }
        }
        public string authorsFilter
        {
            get { return _authorsFilter; }
            set { _authorsFilter = value; OnPropertyChanged("authorsFilter"); }
        }
        public int? yearFilter
        {
            get { return _yearFilter; }
            set { _yearFilter = value; OnPropertyChanged("yearFilter"); }
        }
        public List<string> typeFilter { get; set; }
        public string selectedTypeFilter
        {
            get { return _selectedTypeFilter; }
            set { _selectedTypeFilter = value; OnPropertyChanged("selectedTypeFilter"); }
        }

        // Bookmark popup
        public bool bookmarkManagerVisibility
        {
            get { return _bookmarkManagerVisibility; }
            set { _bookmarkManagerVisibility = value; OnPropertyChanged("bookmarkManagerVisibility"); }
        }
        public List<Bookmark> bookmarks
        {
            get { return _bookmarks; }
            set { _bookmarks = value; OnPropertyChanged("bookmarks"); }
        }
        public Book managedBook
        {
            get { return _managedBook; }
            set { _managedBook = value; OnPropertyChanged("managedBook"); }
        }
        public string bookmarkName
        {
            get { return _bookmarkName; }
            set { _bookmarkName = value; OnPropertyChanged("bookmarkName"); }
        }

        public bool bookmarkNameFocus
        {
            get { return _bookmarkNameFocus; }
            set { _bookmarkNameFocus = value; OnPropertyChanged("bookmarkNameFocus"); }
        }



        public ICommand showAddDataCommand { get; set; }
        public ICommand closeAddDataCommand { get; set; }
        public ICommand selectFileCommand { get; set; }
        public ICommand addBookCommand { get; set; }
        public ICommand findBooksCommand { get; set; }
        public ICommand resetFilterCommand { get; set; }
        public ICommand enterEditModeCommand { get; set; }
        public ICommand exitEditModeCommand { get; set; }
        public ICommand updateBookCommand { get; set; }
        public ICommand deleteBookCommand { get; set; }
        public ICommand openBookmarkManagerCommand { get; set; }
        public ICommand closeBookmarkManagerCommand { get; set; }
        public ICommand manageBookCommand { get; set; }
        public ICommand createBookmarkCommand { get; set; }
        public ICommand openFileCommand { get; set; }

        public BooksViewModel(Action<bool> working) : base(working)
        {
            addDataVisibility = false;
            books = new List<Book>();
            bookTypes = new List<string>()
            {
                "book",
                "phd"
            };
            type = bookTypes[0];
            typeFilter = new List<string>()
            {
                "All",
                "Book",
                "PHD"
            };
            selectedTypeFilter = typeFilter[0];
            authors = new ObservableCollection<string>();

            showAddDataCommand = new RelayCommand(showAddData);
            closeAddDataCommand = new RelayCommand(closeAddData);
            selectFileCommand = new RelayCommand(selectFile);
            addBookCommand = new RelayCommand(addBook);
            findBooksCommand = new RelayCommand(findBooks);
            resetFilterCommand = new RelayCommand(resetFilter);
            openFileCommand = new RelayCommand(openFile);
            enterEditModeCommand = new RelayCommand(enterEditMode);
            exitEditModeCommand = new RelayCommand(exitEditMode);
            updateBookCommand = new RelayCommand(updateBook);
            deleteBookCommand = new RelayCommand(deleteBook);
            openBookmarkManagerCommand = new RelayCommand(openBookmarkManager);
            closeBookmarkManagerCommand = new RelayCommand(closeBookmarkManager);
            manageBookCommand = new RelayCommand(manageBook);
            createBookmarkCommand = new RelayCommand(createBookmark, canCreateBookmark);

            string pathFiles = Path.Combine(Environment.CurrentDirectory, "Files");
            if (!Directory.Exists(pathFiles))
                Directory.CreateDirectory(pathFiles);
        }

        public void showAddData(object input)
        {
            addDataVisibility = true;
            nameFocus = true;
        }
        public void closeAddData(object input)
        {
            addDataVisibility = false;
            clearAddData();
        }
        public void selectFile(object input = null)
        {
            file = selectFilePath();
        }
        public async void addBook(object input = null)
        {
            createFailed = true;
            OnPropertyChanged(null);
            working(true);
            await Task.Run(() =>
            {
                BookRepo repo = new BookRepo();

                checkRequired();

                // Add book
                string extension = Path.GetExtension(file);
                string joinedAuthors = String.Join(", ", authors);
                string fileName = repo.create(new Book(name, joinedAuthors, year, type, extension));

                // Copy & move file
                string copyFile = fileName + extension;
                string donePath = Path.Combine(Path.GetDirectoryName(file), "Done");
                if (!Directory.Exists(donePath)) Directory.CreateDirectory(donePath);
                File.Copy(file, Path.Combine(Environment.CurrentDirectory, "Files", copyFile));
                File.Move(file, Path.Combine(donePath, Path.GetFileName(file)));

                // Manage bookmarks
                addDataVisibility = false;
                Book book = repo.find(new BookFilter() { name = name })[0];
                toggled = true;
                nameFocus = false;
                openBookmarkManagerCommand.Execute(book);
            });

            // Clear data
            clearAddData();
            createFailed = false;
            OnPropertyChanged(null);

            working(false);
        }
        public async void findBooks(object input = null)
        {
            working(true);
            await Task.Run(() =>
            {
                string typeVal = selectedTypeFilter == "All" ? null : selectedTypeFilter.ToLower();
                books = new BookRepo().find(new BookFilter() { name = nameFilter, authors = authorsFilter, year = yearFilter, type = typeVal });
            });
            working(false);
        }
        public void resetFilter(object input = null)
        {
            nameFilter = null;
            authorsFilter = null;
            yearFilter = null;
            selectedTypeFilter = typeFilter[0];
            books = new List<Book>();
        }
        public void openFile(object input)
        {
            if (input == null) return;

            Book book = input as Book;
            Process.Start(Path.Combine(Environment.CurrentDirectory, "Files", book.file + book.extension));
        }
        public void enterEditMode(object input)
        {
            Book book = input as Book;
            book.editMode = true;
            book.originalName = book.name;
        }
        public void exitEditMode(object input)
        {
            Book book = input as Book;
            book.editType = null;
            book.editMode = false;
        }
        public async void updateBook(object input)
        {
            working(true);
            await Task.Run(() =>
            {
                Book book = input as Book;
                try
                {
                    book.type = book.editType;
                    new BookRepo().findByIdAndUpadte(book);
                    book.editMode = false;
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("UNIQUE"))
                    {
                        book.name = book.originalName;
                    }

                    throw e;
                }
            });
            working(false);
        }
        public async void deleteBook(object input)
        {
            Book book = input as Book;
            MessageBoxResult dialogResult = MessageBox.Show($"Are you sure you want to delete {book.name}?", "Delete book", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                working(true);
                await Task.Run(() =>
                {
                    new BookRepo().findByIdAndDelete(book.id);
                    File.Delete(Path.Combine(Environment.CurrentDirectory, "Files", book.file + book.extension));
                    findBooksCommand.Execute(null);
                });
                working(false);
            }
        }
        public async void openBookmarkManager(object input)
        {
            working(true);
            await Task.Run(() =>
            {
                Book book = input as Book;
                loadBookmarks(book);
                managedBook = book;
                bookmarkManagerVisibility = true;
            });
            working(false);
        }
        public void closeBookmarkManager(object input = null)
        {
            bookmarks = null;
            managedBook = null;
            bookmarkManagerVisibility = false;

            // Reopen add data popup if manager was called from it
            if (toggled)
            {
                addDataVisibility = true;
                nameFocus = true;
            }

            toggled = false;
                
        }
        public async void manageBook(object input)
        {
            working(true);
            await Task.Run(() =>
            {
                Bookmark bookmark = input as Bookmark;

                if (bookmark.hasBook)
                {
                    new BookmarkRepo().removeBook(bookmark, managedBook);
                }
                else
                {
                    new BookmarkRepo().addBook(bookmark, managedBook);
                }

                bookmark.hasBook = !bookmark.hasBook;
            });
            working(false);
        }
        public async void createBookmark(object input = null)
        {
            working(true);
            await Task.Run(() =>
            {
                new BookmarkRepo().create(new Bookmark(bookmarkName));
                bookmarkName = null;
                loadBookmarks(managedBook);
                bookmarkNameFocus = true;
            });
            working(false);
        }

        public bool canCreateBookmark(object inpu = null)
        {
            return !string.IsNullOrWhiteSpace(bookmarkName);
        }

        // Helper functions
        private void clearAddData()
        {
            name = null;
            year = null;
            file = null;
            authors.Clear();
        }
        private void checkRequired()
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(type) || string.IsNullOrEmpty(file))
            {
                throw new Exception("Please fill in all required fields!");
            }

            if (year != null && !numbersOnly.IsMatch(year.ToString()))
                throw new Exception("Year must be a number!");
        }
        private string selectFilePath()
        {
            // Create OpenFileDialog 
           OpenFileDialog dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".png";
            //dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Get the selected file name and display in a TextBox 
            if (dlg.ShowDialog() == true)
            {
                // Open document 
                return dlg.FileName;
            }

            return file;
        }
        private void loadBookmarks(Book book)
        {
            List<Bookmark> raw = new BookmarkRepo().find(null);
            raw.ForEach((Bookmark bookmark) =>
            {
                bookmark.checkBook(book);
            });
            bookmarks = raw;
        }
    }
}
