using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.BookRegistration.Entity
{
    public class Book
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
