using BookBase.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBase.Models
{
    class Bookmark : BaseModel
    {
        private int _booksCount;
        private bool _editMode;
        private bool _hasBook;

        public int id { get; set; }
        public string name { get; set; }

        public int booksCount
        {
            get { return _booksCount; }
            set { _booksCount = value; OnPropertyChanged("booksCount"); }
        }
        public bool editMode
        {
            get { return _editMode; }
            set { _editMode = value; OnPropertyChanged("editMode"); }
        }
        public bool hasBook
        {
            get { return _hasBook; }
            set { _hasBook = value; OnPropertyChanged("hasBook"); }
        }

        public Bookmark()
        {

        }

        public Bookmark(string name)
        {
            this.name = name;
            booksCount = 5;
        }

        public void countBooks()
        {
            int count = new BookmarkRepo().getBookCount(name);
            booksCount = count;
        }
        public void checkBook(Book book)
        {
            hasBook = new BookmarkRepo().checkBook(this, book);
        }
    }
}
