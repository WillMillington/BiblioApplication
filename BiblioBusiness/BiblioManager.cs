using System;
using Biblio.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioBusiness
{
    class BiblioManager
    {
        static void Main(string[] args)
        {
            using (var db = new BiblioContext())
            {
                var Query = db.Books.Include(a => a.Author);
                foreach (var book in Query)
                {
                    Console.WriteLine($"{book} by {book.Author}");
                }
            }
        }
    }
}
