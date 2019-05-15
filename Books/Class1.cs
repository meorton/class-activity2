using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    class Book
    {
        public string nameBook { get; set; }
        public List<Author> authors { get; set; }
        public List<PublishingCompany> publishingCompanies { get; set; }
        public int yearPublication { get; set; }
        public decimal Price { get; set; }
        public int stock { get; set; }

        public void newbook()
        {
            Book newbook = new Book();
            newbook.nameBook = "Object-Oriented Programming with C#";
            newbook.authors = new List<Author>();
            newbook.publishingCompanies = new List<PublishingCompany>();
            newbook.yearPublication = 2013;
            newbook.Price = 47.99M;
            newbook.stock = 10;
        }
    }
       
}
