using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabPRN
{
    public class BookDTO
    {
        public string BookID { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Describe { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public DateTime YearProduction { get; set; }

        public bool Status { get; set; }

        public BookDTO(string bookID, string title, double price, string image, string describe, string authorName, string category, int quantity,  string publisher, DateTime yearProduction, bool status)
        {
            BookID = bookID;
            Title = title;
            Price = price;
            Image = image;
            Describe = describe;    
            AuthorName = authorName;
            Quantity = quantity;
            Category = category;
            Publisher = publisher;
            YearProduction = yearProduction;
            Status = status;
        }

        public BookDTO()
        {
        }
    }
}
