using BookBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Dapper;
using BookBase.Utils;

namespace BookBase.Repositories
{
    class BookmarkRepo : BaseRepo<Bookmark>
    {
        public override string create(Bookmark record)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                conn.Execute("INSERT INTO Bookmark (name) VALUES(@name)",
                    new { record.name });

                return null;
            }
        }
        public override List<Bookmark> find(IFilter filter)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                return conn.Query<Bookmark>("SELECT id, name FROM Bookmark").ToList();
            }
        }
        public override Bookmark findById(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                return conn.QuerySingleOrDefault<Bookmark>("SELECT id, name FROM bookmark WHERE id=@id",
                    new { id });
            }
        }
        public override void findByIdAndUpadte(Bookmark record)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                conn.Execute("UPDATE Bookmark SET name=@name WHERE id=@id",
                    new { record.name, record.id });
            }
        }
        public override void findByIdAndDelete(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                conn.ExecuteAsync("DELETE FROM BookBookmark WHERE bookmarkId=@bookmarkId; DELETE FROM bookmark WHERE id=@id;",
                    new { bookmarkId = id, id = id });
            }
        }

        public int getBookCount(string bookmark)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string queryString = @"
SELECT COUNT(b.name) AS books FROM Bookmark AS bkm
LEFT JOIN BookBookmark as bb ON bkm.id = bb.bookmarkId
LEFT JOIN Book as b ON bb.bookId = b.id
WHERE bkm.name = @bookmark;";
                return conn.QuerySingleOrDefault<int>(queryString, new { bookmark });
            }
        }
        public List<Book> getBooks(string bookmark)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string queryString = @"
SELECT b.id, b.name, b.authors, b.year, b.type, b.file, b.extension  FROM Book AS b
JOIN BookBookmark AS bb ON b.id = bb.bookId
JOIN Bookmark AS bkm ON bb.bookmarkId = bkm.id
WHERE bkm.name = @bookmark;";
                return conn.Query<Book>(queryString, new { bookmark }).ToList();
            }
        }
        public void addBook(Bookmark bookmark, Book book)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                conn.Execute("INSERT INTO BookBookmark (bookId, bookmarkId) VALUES (@bookId, @bookmarkId)",
                    new { bookId = book.id, bookmarkId = bookmark.id, });
            }
        }
        public void removeBook(Bookmark bookmark, Book book)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                conn.Execute("DELETE FROM BookBookmark WHERE bookmarkId=@bookmarkId AND bookId=@bookId",
                    new { bookmarkId = bookmark.id, bookId = book.id });
            }
        }
        public bool checkBook(Bookmark bookmark, Book book)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                int count = conn.QuerySingleOrDefault<int>("SELECT COUNT(bookId) AS num FROM BookBookmark WHERE bookId=@bookId AND bookmarkId=@bookmarkId",
                    new { bookId = book.id, bookmarkId = bookmark.id });

                if (count > 0)
                    return true;

                return false;
            }
        }
    }
}
