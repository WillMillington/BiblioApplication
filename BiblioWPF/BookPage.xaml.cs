using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Biblio.Models;
using BiblioBusiness;

namespace BiblioWPF
{
    public partial class BookPage : Page
    {
        private BiblioManager _bibManager = new BiblioManager();
        public BookPage(object selectedBook)
        {
            InitializeComponent();
            _bibManager.SetSelectedBook(selectedBook);
            PopulateFields();
        }
        private void Book_Home_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Source = null;
        }
        public void PopulateFields()
        {
            var book = _bibManager.SelectedBook;
            SelectedBookTitle.Text = book.Title;
            SelectedBookAuthor.Text = book.Author.ToString();
            SelectedBoonPage.Text = (book.NumOfPages != 0) ? $"{book.NumOfPages.ToString()} pages." : null;
            SelectedBookISBN10.Text = (book.Isbn10 != null) ? $"ISBN 10: {book.Isbn10}" : null;
            SelectedBookISBN13.Text = (book.Isbn13 != null) ? $"ISBN 13: {book.Isbn10}" : null;
            SelectedBookDescriptionTitle.Text = (book.Description != null) ? "Description:" : null;
            SelectedBookDescription.Text = (book.Description != null) ? book.Description : null;
            SelectedBookCopies.Text = (book.NumOfCopies > 1) ? $"You have {book.NumOfCopies} copies of this book." : null;
            SelectedBookRatingTitle.Text = (book.Review == 0) ? null : "Rating:";
            SelectedBookRead.Text = (book.Read == true) ? "You have read this book." : null;

            switch(book.Review)
            {
                case 0:
                    SelectedBookRating.Text = null;
                    break;
                case 1:
                    SelectedBookRating.Text = "&";
                    break;
                case 2:
                    SelectedBookRating.Text = "&&";
                    break;
                case 3:
                    SelectedBookRating.Text = "&&&";
                    break;
                case 4:
                    SelectedBookRating.Text = "&&&&";
                    break;
                case 5:
                    SelectedBookRating.Text = "&&&&&";
                    break;
            }

            if (book.Publisher == null && book.PublishedDate == null)
            {
                SelectedBookPublish.Text = "No publisher entered.";
            }
            else if (book.PublishedDate == null)
            {
                SelectedBookPublish.Text = $"Published by {book.Publisher}.";
            }
            else if (book.Publisher == null)
            {
                SelectedBookPublish.Text = $"Published in {book.PublishedDate}";
            }
            else
            {
                SelectedBookPublish.Text = $"Published by {book.Publisher} in {book.PublishedDate}";
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditBook editWindow = new EditBook(_bibManager.SelectedBook);
            this.NavigationService.Source = null;
            editWindow.Show();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteMessage = new DeleteWindow(_bibManager.SelectedBook);
            deleteMessage.Show();
            this.NavigationService.Source = null;
        }
    }
}
