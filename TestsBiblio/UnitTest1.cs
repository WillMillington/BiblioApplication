//using NUnit.Framework;
//using BiblioBusiness;
//using System.Linq;

//namespace TestsBiblio
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void OrderByTitleTest()
//        {
//            var resultFirst = BiblioManager.GetAllBooksByTitle().First();
//            var resultLast = BiblioManager.GetAllBooksByTitle().Last();
//            Assert.AreEqual(resultFirst.Title, "A Brief History of Time");
//            Assert.AreEqual(resultLast.Title, "The Pleasure of Finding Things Out");
//        }

//        [Test]
//        public void OrderByAuthorTest()
//        {
//            var resultFirst = BiblioManager.GetAllBooksByAuthor().First();
//            var resultLast = BiblioManager.GetAllBooksByAuthor().Last();
//            Assert.AreEqual(resultFirst.Author.LastName, "Feynman");
//            Assert.AreEqual(resultLast.Author.LastName, "Leavitt");
//        }

//        [Test]
//        public void OrderByAddedTest()
//        {
//            var resultFirst = BiblioManager.GetAllBooksByAdded().First();
//            var resultLast = BiblioManager.GetAllBooksByAdded().Last();
//            Assert.AreEqual(resultFirst.BookId, 2);
//            Assert.AreEqual(resultLast.BookId, 9);
//        }
//    }
//}