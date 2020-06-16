using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Biblio.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioBusiness
{
    public class BiblioManager
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("By Title------------------------------");
            var titleResult = GetAllBooksByTitle();
            foreach (var book in titleResult)
            {
                Console.WriteLine($"{book} by {book.Author} published in {book.PublishedDate}");
            }
            Console.WriteLine("");
            Console.WriteLine("By Author------------------------------");
            var authorResult = GetAllBooksByAuthor();
            foreach (var book in authorResult)
            {
                Console.WriteLine($"{book} by {book.Author} published in {book.PublishedDate}");
            }
            Console.WriteLine("");
            Console.WriteLine("By Added------------------------------");
            var addedResult = GetAllBooksByAdded();
            foreach (var book in addedResult)
            {
                Console.WriteLine($"{book} by {book.Author} published in {book.PublishedDate}");
            }
        }
        public static List<Books> GetAllBooksByTitle()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.Title).ToList();
            }  
        }
        public static List<Books> GetAllBooksByAuthor()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(a => a.Author.LastName).ThenBy(b => b.Title).ToList();
            }
        }
        public static List<Books> GetAllBooksByAdded()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.BookId).ToList();
            }
        }


    }
}
