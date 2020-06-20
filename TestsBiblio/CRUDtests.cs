using NUnit.Framework;
using BiblioBusiness;
using System.Linq;
using Biblio.Models;
using System.Runtime.CompilerServices;
using NUnit.Framework.Internal;

namespace TestsBiblio
{
    public class CRUDTests
    {
        private BiblioManager _testBiblio = new BiblioManager();
                
        public string Title = "A Aardvark Attractiveness Album";
        string AuthorFirst = "Aaron A.";
        string AuthorLast = "Aardvark";
        string Isbn10 = "0000000000";
        string Isbn13 = "0000000000000";
        int NumOfPages = 200;
        bool Read = true;
        int Review = 5;
        string PublishedDate = "2020";
        string Publisher = "Aardvark 'ardbacks";
        string Description = "Aardvark attractiveness alternates accorinding to austerity apparentley, as acknowledged by Aardvark 'ardbacks anyway.";

        [Test]
        public void AddBookTestNewBook()
        {
            int preAddTestBooks = 0;
            int preAddTestAuthor = 0;
            int postAddTestBooks = 0;
            int postAddTestAuthor = 0;
            using (var db = new BiblioContext())
            {
                preAddTestBooks = db.Books.Count();
                preAddTestAuthor = db.Authors.Count();
            }

            _testBiblio.AddBook(AuthorFirst, AuthorLast, Title, Isbn10, Isbn13, Publisher, PublishedDate, NumOfPages, Description, Review, Read);

            using (var db = new BiblioContext())
            {
                postAddTestBooks = db.Books.Count();
                postAddTestAuthor = db.Authors.Count();
            }
            Assert.AreEqual(preAddTestBooks, postAddTestBooks - 1);
            Assert.AreEqual(preAddTestAuthor, postAddTestAuthor - 1);
            using (var db = new BiblioContext())
            {
                var removeBook = db.Books.OrderByDescending(b => b.BookId).First();
                var removeAuthor = db.Authors.OrderByDescending(a => a.AuthorId).First();
                db.Remove(removeBook);
                db.SaveChanges();
                db.Remove(removeAuthor);
                db.SaveChanges();
            }
        }
        [Test]
        public void AddBookTestExistingBook()
        {
            int preAddTestBooks = 0;
            int preAddTestAuthor = 0;
            int preNumCopies = 0;
            int postAddTestBooks = 0;
            int postAddTestAuthor = 0;
            int postNumCopies = 0;
            using (var db = new BiblioContext())
            {
                Authors testAuthor = new Authors
                {
                    FirstName = "Aaron A.",
                    LastName = "Aardvark"
                };
                db.Add(testAuthor);
                db.SaveChanges();
                int authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                Books testBook = new Books
                {
                    Title = "A Aardvark Attractiveness Album",
                    AuthorId = authorId,
                    Isbn10 = "0000000000",
                    Isbn13 = "0000000000000",
                    NumOfCopies = 1,
                    NumOfPages = 200,
                    Read = true,
                    Review = 5,
                    PublishedDate = "2020",
                    Publisher = "Aardvark 'ardbacks",
                    Description = "Aardvark attractiveness alternates accorinding to austerity apparentley, as acknowledged by Aardvark 'ardbacks anyway."
                };
                db.Add(testBook);
                db.SaveChanges();
                preAddTestBooks = db.Books.Count();
                preAddTestAuthor = db.Authors.Count();
                preNumCopies = db.Books.Where(b => b.Title == "A Aardvark Attractiveness Album").First().NumOfCopies;
            }

            _testBiblio.AddBook(AuthorFirst, AuthorLast, Title, Isbn10, Isbn13, Publisher, PublishedDate, NumOfPages, Description, Review, Read);

            using (var db = new BiblioContext())
            {
                postAddTestBooks = db.Books.Count();
                postAddTestAuthor = db.Authors.Count();
                postNumCopies = db.Books.Where(b => b.Title == "A Aardvark Attractiveness Album").First().NumOfCopies;
            }
            Assert.AreEqual(preAddTestBooks, postAddTestBooks);
            Assert.AreEqual(preAddTestAuthor, postAddTestAuthor);
            Assert.AreEqual(postNumCopies - 1, preNumCopies);
            using (var db = new BiblioContext())
            {
                var removeBook = db.Books.OrderByDescending(b => b.BookId).First();
                var removeAuthor = db.Authors.OrderByDescending(a => a.AuthorId).First();
                db.Remove(removeBook);
                db.SaveChanges();
                db.Remove(removeAuthor);
                db.SaveChanges();
            }
        }
        [Test]
        public void EditBookTest()
        {
            using (var db = new BiblioContext())
            {
                Authors testAuthor = new Authors
                {
                    FirstName = "Aaron A.",
                    LastName = "Aardvark"
                };
                db.Add(testAuthor);
                db.SaveChanges();
                int authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                Books testBook = new Books
                {
                    Title = "A Aardvark Attractiveness Album",
                    AuthorId = authorId,
                    Isbn10 = "0000000000",
                    Isbn13 = "0000000000000",
                    NumOfCopies = 1,
                    NumOfPages = 200,
                    Read = true,
                    Review = 5,
                    PublishedDate = "2020",
                    Publisher = "Aardvark 'ardbacks",
                    Description = "Aardvark attractiveness alternates accorinding to austerity apparentley, as acknowledged by Aardvark 'ardbacks anyway."
                };
                db.Add(testBook);
                db.SaveChanges();
                _testBiblio.SetSelectedBook(testBook);
                
            }
            string editDescription = "new description";
            int editReview = 4;
            bool editRead = false;
            _testBiblio.EditBook("Aaron a.", "Aardvark", "An Aardvark Attractiveness Album", "0000000000", "0000000000000", "Aardvark 'ardbacks", "2020", 200, editDescription, editReview, editRead);

            using (var db = new BiblioContext())
            {
                string newDescription = db.Books.OrderByDescending(b => b.BookId).First().Description;
                int newReview = db.Books.OrderByDescending(b => b.BookId).First().Review;
                bool newRead = db.Books.OrderByDescending(b => b.BookId).First().Read;
            
                Assert.AreEqual(editDescription, newDescription);
                Assert.AreEqual(editReview, newReview);
                Assert.AreEqual(editRead, newRead);
            
                var removeBook = db.Books.OrderByDescending(b => b.BookId).First();
                var removeAuthor = db.Authors.OrderByDescending(a => a.AuthorId).First();
                db.Remove(removeBook);
                db.SaveChanges();
                db.Remove(removeAuthor);
                db.SaveChanges();
            }
        }
        [Test]
        public void DeleteBookTest()
        {
            Books selectedBook;
            using (var db = new BiblioContext())
            {
                Authors testAuthor = new Authors
                {
                    FirstName = "Aaron A.",
                    LastName = "Aardvark"
                };
                db.Add(testAuthor);
                db.SaveChanges();
                int authorId = db.Authors.OrderByDescending(a => a.AuthorId).First().AuthorId;
                Books testBook = new Books
                {
                    Title = "A Aardvark Attractiveness Album",
                    AuthorId = authorId,
                    Isbn10 = "0000000000",
                    Isbn13 = "0000000000000",
                    NumOfCopies = 1,
                    NumOfPages = 200,
                    Read = true,
                    Review = 5,
                    PublishedDate = "2020",
                    Publisher = "Aardvark 'ardbacks",
                    Description = "Aardvark attractiveness alternates accorinding to austerity apparentley, as acknowledged by Aardvark 'ardbacks anyway."
                };
                db.Add(testBook);
                db.SaveChanges();
                selectedBook = testBook;
            }
            _testBiblio.DeleteBook(selectedBook);
            using (var db = new BiblioContext())
            {
                int resultBook = db.Books.Where(b => b.Title == "A Aardvark Attractiveness Album").Count();
                int resultAuthor = db.Authors.Where(a => a.LastName == "Aardvark" && a.FirstName == "Aaron A.").Count();

                Assert.AreEqual(resultBook, 0);
                Assert.AreEqual(resultAuthor, 1);

                var removeAuthor = db.Authors.OrderByDescending(a => a.AuthorId).First();
                db.SaveChanges();
                db.Remove(removeAuthor);
                db.SaveChanges();
            }
        }
    }
}

