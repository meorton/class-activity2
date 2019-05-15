using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books
{
    class Book
    {
        public string nameCar { get; set; }
        public List<models> models { get; set; }
        public List<CarCompanies> CarCompanies { get; set; }
        public int yearPublication { get; set; }
        public decimal Price { get; set; }
        public int stock { get; set; }

        public void newbook()
        {
            Book newbook = new Book();
            newbook.nameCar = "Object-Oriented Programming with C#";
            newbook.models = new List<models>();
            newbook.CarCompanies = new List<CarCompanies>();
            newbook.yearPublication = 2013;
            newbook.Price = 47.99M;
            newbook.stock = 10;
        }
    }
       
}
