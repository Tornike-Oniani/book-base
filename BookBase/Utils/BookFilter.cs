using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBase.Utils
{
    class BookFilter : IFilter
    {
        private string query;

        public string name { get; set; }
        public string authors { get; set; }
        public int? year { get; set; }
        public string type { get; set; }

        public string generateQuery()
        {
            if (exists()) return "";

            query = " WHERE ";

            // 1. Add name
            addCheck("name", name);

            // 2. Add authors
            if (!string.IsNullOrEmpty(authors))
            {
                List<string> authorsList = authors.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
                authorsList.ForEach(author =>
                {
                    addCheck("authors", author);
                });
            }

            // 3. Add year
            addCheck("year", year);

            // 4. Add type
            addCheck("type", type);

            // 4. Remove spare AND
            query = query.Remove(query.Length - 4, 4);

            return query;
        }

        // Private helpers
        private bool exists()
        {
            return string.IsNullOrEmpty(name) && string.IsNullOrEmpty(authors) && year == null && type == null;
        }
        private void addCheck(string property, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            string reworked = value.Replace("'", "''");
            query += $" {property} LIKE '%{reworked}%' AND";
        }

        private void addCheck(string property, int? value)
        {
            if (value == null) return;

            query += $" {property} = {value} AND";
        }
    }
}
