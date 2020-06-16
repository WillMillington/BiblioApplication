using System;
using System.Collections.Generic;

namespace Biblio.Models
{
    public partial class Books
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public int? NumOfPages { get; set; }
        public string Description { get; set; }
        public double? Review { get; set; }
        public int NumOfCopies { get; set; }
        public bool? Read { get; set; }
        public int AuthorId { get; set; }

        public virtual Authors Author { get; set; }
        public override string ToString()
        {
            return Title;
        }
    }
}
