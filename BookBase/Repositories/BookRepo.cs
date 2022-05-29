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
    class BookRepo : BaseRepo<Book>
    {
        public override string create(Book record)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string file = generateFileName(conn);
                conn.Execute("INSERT INTO Book (name, authors, year, type, file, extension) VALUES(@name, @authors, @year, @type, @file, @extension)", 
                    new { record.name, record.authors, record.year, record.type, file, record.extension });

                return file;
            }
        }
        public override List<Book> find(IFilter filter)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, name, authors, year, type, file, extension FROM Book";
                query += filter.generateQuery();
                return conn.Query<Book>(query).ToList();
            }
        }
        public override Book findById(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                return conn.QuerySingleOrDefault<Book>("SELECT id, name, authors, year, type, file FROM Book WHERE id=@id", 
                    new { id });
            }
        }
        public override void findByIdAndUpadte(Book record)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                conn.Execute("UPDATE Book SET name=@name, authors=@authors, year=@year, type=@type WHERE id=@id",
                    new { record.name, record.authors, record.year, record.type, record.id });
            }
        }
        public override void findByIdAndDelete(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                conn.Execute("DELETE FROM book WHERE id=@id",
                    new { id });
            }
        }

        public bool checkBookByName(string name, out Book book)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                Book foundBook = conn.QuerySingleOrDefault<Book>("SELECT id, file, extension  FROM Book WHERE name=@name", 
                    new { name });

                book = foundBook;

                if (foundBook == null)
                    return false;

                return true;
            }
        }

        private string generateFileName(SQLiteConnection conn)
        {
            string model = "000000";
            int id = conn.QuerySingleOrDefault<int>("SELECT seq FROM sqlite_sequence WHERE name = 'Book';");
            string nextId = (id + 1).ToString();
            string concat = model.Substring(0, model.Length - nextId.Length) + nextId;
            string final = concat.Substring(0, concat.Length / 2) + "-" + concat.Substring(concat.Length / 2, concat.Length / 2);
            return final;
        }
    }
}
