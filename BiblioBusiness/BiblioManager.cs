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
        //Method that lists all the books ordered alphabetically by the title.
        public List<Books> GetAllBooksByTitle()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.Title).ToList();
            }
        }

        //Method that lists all the books ordered alphabetically by the surname of the author and then by the title.
        public List<Books> GetAllBooksByAuthor()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(a => a.Author.LastName).ThenBy(b => b.Title).ToList();
            }
        }

        //Method that lists all the books ordered by when they were added to the database.
        public List<Books> GetAllBooksByAdded()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.Include(a => a.Author).OrderBy(b => b.BookId).ToList();
            }
        }

        //Method that returns a count of the number of books in the library.
        public int GetCount()
        {
            using (var db = new BiblioContext())
            {
                return db.Books.ToList().Count();
            }
        }

        //Method that uses string.Contains() to search the book titles, author's name (and title) for the input of the search bar and returns the books.
        public List<Books> SearchBooks(string input)
        {
            using (var db = new BiblioContext())
            {
                string correctInput = input.ToUpper().Trim();
                List<Books> searchResult = db.Books.Include(a => a.Author).Where(b => b.Title.ToUpper().Contains(correctInput) || b.Author.LastName.Contains(correctInput) || b.Author.FirstName.Contains(correctInput) || b.Author.Title.Contains(correctInput)).OrderBy(b => b.Title).ToList();
                return (searchResult.Count() == 0)? null : searchResult;
            }
        }

        //Method that adds books to the database
        //1)    Checks if the author is in the database
        //2a)   If author is in then author Id is set to the inputted author
        //2b)   If author isn't in then the author is added and author Id is set to the latest one in the database
        //2b.1) If the firstname contains Dr. or Prof. then that is separated and added as author Title.
        //3)    Checks if the book is in the database
        //3a)   If book is in then nothing is added
        //3b)   If book isnt then the book is added with inputted data
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
        
        //Method that takes in the selected item from the WPF layer and sets it as the variable selectedBook for use in methods.
        public Books SetSelectedBook(object selectedItem)
        {
            SelectedBook = (Books)selectedItem;
            return SelectedBook;
        }
        
        //Method that edits books in the database
        //1) Finds the book and author entries in the database
        //2) Updates the data with the inputs on the selected author and book
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
        
        //Method that deletes the selected book
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