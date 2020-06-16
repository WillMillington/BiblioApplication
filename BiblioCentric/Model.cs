using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace BiblioCentric
{
    class BiblioContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Biblio;Integrated Security=True; ConnectRetryCount=0");
    }

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Publisher { get; set; }
        public string PublishedDate { get; set; }
        public int? NumOfPages { get; set; }
        public string Description { get; set; }
        public int? Review { get; set; }
        public int NumOfCopies { get; set; } = 1;
        public bool? Read { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }

    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public List<Book> Books { get; } = new List<Book>();
    }
}