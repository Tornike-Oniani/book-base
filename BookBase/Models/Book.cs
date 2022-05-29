using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBase.Models
{
    class Book : BaseModel
    {
        private bool _editMode;
        private string _editType;
        private string _name;

        public int id { get; set; } 
        public string name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("name"); OnPropertyChanged("isValid"); }
        }
        public string authors { get; set; }
        public int? year { get; set; }
        public string type { get; set; }
        public string file { get; set; }
        public string extension { get; set; }
        public string editType
        {
            get 
            { 
                if (_editType != null) return _editType;
                return type;
            }
            set { _editType = value; }
        }

        public bool editMode
        {
            get { return _editMode; }
            set { _editMode = value; OnPropertyChanged("editMode"); }
        }
        public string originalName { get; set; }
        public bool isValid 
        {
            get { return !string.IsNullOrEmpty(name); }
        }

        public Book()
        {

        }

        public Book(string name, 
                    string authors, 
                    int? year, 
                    string type, 
                    string extension)
        {
            this.name = name;
            this.authors = authors;
            this.year = year;
            this.type = type;
            this.extension = extension;
        }
    }
}
