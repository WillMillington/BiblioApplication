using NUnit.Framework;
using BiblioBusiness;
using System.Linq;
using Biblio.Models;
using System.Runtime.CompilerServices;

namespace TestsBiblio
{
    public class Tests
    {
        private BiblioManager _testBiblio = new BiblioManager();
        [Test]
        public void OrderByTitleTest()
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
            }

            var result = _testBiblio.GetAllBooksByTitle().First();
            Assert.AreEqual(result.Title, "A Aardvark Attractiveness Album");
            Assert.AreEqual(result.Author.FirstName, "Aaron A.");
            Assert.AreEqual(result.Author.LastName, "Aardvark");

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
        public void OrderByAuthorTest()
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
            }

            var result = _testBiblio.GetAllBooksByAuthor().First();
            Assert.AreEqual(result.Title, "A Aardvark Attractiveness Album");
            Assert.AreEqual(result.Author.LastName, "Aardvark");
            Assert.AreEqual(result.Author.FirstName, "Aaron A.");

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
        public void OrderByAddedTest()
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
            }

            var result = _testBiblio.GetAllBooksByAdded().Last();
            Assert.AreEqual(result.Title, "A Aardvark Attractiveness Album");
            Assert.AreEqual(result.Author.FirstName, "Aaron A.");
            Assert.AreEqual(result.Author.LastName, "Aardvark");

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
        public void CountTest()
        {
            int preAddTest = _testBiblio.GetCount();
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
            }            
            int postAddTest = _testBiblio.GetCount();
            Assert.AreEqual(preAddTest, postAddTest - 1);

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
        public void SearchTest()
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
            }
            var searchTest = _testBiblio.SearchBooks("Aardvark");
            Assert.AreEqual(searchTest.Count(), 1);

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
    }
}
