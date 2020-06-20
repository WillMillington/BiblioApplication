using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Biblio.Models;
using Microsoft.EntityFrameworkCore;

namespace BiblioBusiness
{
    public class BiblioManager
    {
        public Books newBook = new Books();
        public Authors newAuthor = new Authors();
        public Books SelectedBook { get; set; }
        public static void Main(string[] args)
        {
            
        }
        public List<Books> GetAllBooksByTitle()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.Title).ToList();
            }
        }
        public List<Books> GetAllBooksByAuthor()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(a => a.Author.LastName).ThenBy(b => b.Title).ToList();
            }
        }
        public List<Books> GetAllBooksByAdded()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.BookId).ToList();
            }
        }
        public int GetCount()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.ToList().Count();
            }
        }
        public List<Books> SearchBooks(string input)
        {
            using (var db = new BiblioContext())
            {
                string correctInput = input.ToUpper().Trim();
                List<Books> searchResult = db.Books.Include(a => a.Author).Where(b => b.Title.ToUpper().Contains(correctInput) || b.Author.LastName.Contains(correctInput) || b.Author.FirstName.Contains(correctInput) || b.Author.Title.Contains(correctInput)).OrderBy(b => b.Title).ToList();
                return (searchResult.Count() == 0)? null : searchResult;
            }
        }
        public void AddBook(string authorFirst, string authorLast, string bookTitle, string isbn10 = null, string isbn13 = null, string publisher = null, string publishDate = null, int numOfPages = 0, string description = null, int review = 0, bool read = false)
        {
            using (var db = new BiblioContext())
            {
                int authorId;
                var authorQuery = db.Authors.Where(a => a.FirstName.Contains(authorFirst) && a.LastName.Contains(authorLast)).ToList();
                if (authorQuery.Count() == 0)
                {
                    if (authorFirst.ToUpper().Contains("DR."))
                    {
                        Authors author = new Authors
                        {
                            Title = "Dr.",
                            FirstName = authorFirst.Remove(0, 4),
                            LastName = authorLast
                        };
                        db.Add(author);
                        authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                    }
                    else if (authorFirst.ToUpper().Contains("PROF."))
                    {
                        Authors author = new Authors
                        {
                            Title = "Prof.",
                            FirstName = authorFirst.Remove(0, 6),
                            LastName = authorLast
                        };
                        db.Add(author);
                        authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                    }
                    else
                    {
                        Authors author = new Authors
                        {
                            Title = null,
                            FirstName = authorFirst,
                            LastName = authorLast
                        };
                        db.Add(author);
                    }
                    db.SaveChanges();
                    authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                }

                else 
                {
                    authorId = authorQuery.First().AuthorId;
                }
                var bookQuery = db.Books.Where(b => b.Title == bookTitle).ToList();
                if(bookQuery.Count() == 0)
                {
                    Books book = new Books
                    {
                        Title = bookTitle,
                        Isbn10 = isbn10,
                        Isbn13 = isbn13,
                        Publisher = publisher,
                        PublishedDate = publishDate,
                        NumOfPages = numOfPages,
                        NumOfCopies = 1,
                        Description = description,
                        Read = read,
                        AuthorId = authorId,
                        Review = review
                    };
                    db.Add(book);
                }
                else
                {
                    bookQuery.First().NumOfCopies++;
                }
                db.SaveChanges();
            }
        }
        public Books SetSelectedBook(object selectedItem)
        {
            SelectedBook = (Books)selectedItem;
            return SelectedBook;
        }
        public void EditBook(string authorFirst, string authorLast, string bookTitle, string isbn10 = null, string isbn13 = null, string publisher = null, string publishDate = null, int numOfPages = 0, string description = null, int review = 0, bool read = false)
        {
            using (var db = new BiblioContext())
            {
                var authorQuery = db.Authors.Where(a => a.AuthorId == SelectedBook.AuthorId);
                if (authorQuery.Count() != 0)
                {
                    var author = authorQuery.First();
                    author.FirstName = authorFirst;
                    author.LastName = authorLast;
                }
                db.SaveChanges();
                var bookQuery = db.Books.Where(b => b.BookId == SelectedBook.BookId);
                if (bookQuery.Count() != 0)
                {
                    var book = bookQuery.First();
                    book.Title = bookTitle;
                    book.Isbn10 = isbn10;
                    book.Isbn13 = isbn13;
                    book.Publisher = publisher;
                    book.PublishedDate = publishDate;
                    book.NumOfPages = numOfPages;
                    book.Description = description;
                    book.Read = read;
                    book.Review = review;
                }
                db.SaveChanges();
            }
        }
        public void DeleteBook(Books selectedBook)
        {
            using (var db = new BiblioContext())
            {
                var bookQuery = db.Books.Where(b => b.BookId == selectedBook.BookId).ToList().First();
                db.Remove(bookQuery);
                db.SaveChanges();
            }
        }
    }
}