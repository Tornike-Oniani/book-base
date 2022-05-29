using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBase.Utils;
using Dapper;

namespace BookBase.Repositories
{
    abstract class BaseRepo<T>
    {
        protected string connectionString = $"Data Source={Path.Combine(Environment.CurrentDirectory, "database.sqlite3;Version=3;")}";

        public abstract string create(T record);
        public abstract List<T> find(IFilter filter);
        public abstract T findById(int id);
        public abstract void findByIdAndUpadte(T record);
        public abstract void findByIdAndDelete(int id);
    }
}
