using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Books
    {
        public int id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
    }
}