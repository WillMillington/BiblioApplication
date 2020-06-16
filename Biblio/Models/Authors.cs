using System;
using System.Collections.Generic;

namespace Biblio.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Books = new HashSet<Books>();
        }

        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Books> Books { get; set; }
        public override string ToString()
        {
            return $"{Title} {FirstName} {LastName}";
        }
    }
}
