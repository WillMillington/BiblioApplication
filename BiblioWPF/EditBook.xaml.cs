﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BiblioBusiness;
using System.Net;

namespace BiblioWPF
{ 
    public partial class EditBook : Window
    {
        //Sets readbook as false by default
        bool haveRead = false;

        //Creates a new BiblioManager Object to call methods
        BiblioManager _biblioManager = new BiblioManager();
        public EditBook(object selectedBook)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _biblioManager.SetSelectedBook(selectedBook);
            PopulateFields();
        }
        
        //Checks the input in the textbox is numbers and doesn't allow anything else
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = (regex.IsMatch(e.Text));
        }
        
        //Cancels input, closes window but shows error message window first (EditWindow.Xaml)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("All data in this form will be lost. Are you sure you want to leave this page?", "Bibliocentric", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }
       
        //Reads if the checkbox is true or false
        private void Read_Input(object sender, RoutedEventArgs e)
        {
            haveRead = true;
        }
        
        //Populates text boxes with the selected book's information
        public void PopulateFields()
        {
            var book = _biblioManager.SelectedBook;
            BookTitle.Text = book.Title;
            AuthorFirst.Text = book.Author.FirstName;
            AuthorLast.Text = book.Author.LastName;
            Publisher.Text = (book.Publisher != null) ? book.Publisher : null;
            PublishDate.Text = (book.PublishedDate != null) ? book.PublishedDate : null;
            ISBN10.Text = (book.Isbn10 != null) ? book.Isbn10 : null;
            ISBN13.Text = (book.Isbn13 != null) ? book.Isbn10 : null;
            Description.Text = (book.Description != null) ? book.Description : null;
            NumOfPages.Text = (book.NumOfPages == 0) ? null : book.NumOfPages.ToString();
            Review.Text = (book.Review == 0) ? null : book.Review.ToString();
            Read.IsChecked = (book.Read == true) ? true : false;
        }
       
        //Calls the edit book method with the input from the edit book page text boxes
        private void Edit_Button_Click(object sender, RoutedEventArgs e)
            {
                string I10 = (string.IsNullOrEmpty(ISBN10.Text)) ? null : ISBN10.Text;
                string I13 = (string.IsNullOrEmpty(ISBN13.Text)) ? null : ISBN13.Text;
                string Pub = (string.IsNullOrEmpty(Publisher.Text)) ? null : Publisher.Text;
                string PubDate = (string.IsNullOrEmpty(PublishDate.Text)) ? null : PublishDate.Text;
                int Pages = (string.IsNullOrEmpty(NumOfPages.Text)) ? 0 : int.Parse(NumOfPages.Text);
                string Des = (string.IsNullOrEmpty(Description.Text)) ? null : Description.Text;
                int Rating = (string.IsNullOrEmpty(Review.Text) || int.Parse(Review.Text) > 5 || int.Parse(Review.Text) < 0) ? 0 : int.Parse(Review.Text);



                if (string.IsNullOrEmpty(BookTitle.Text) || string.IsNullOrEmpty(AuthorFirst.Text) || string.IsNullOrEmpty(AuthorLast.Text))
                {
                    System.Windows.MessageBox.Show("Please check all required fields are filled in.");
                }
                else
                {
                    _biblioManager.EditBook(AuthorFirst.Text, AuthorLast.Text, BookTitle.Text, I10, I13, Pub, PubDate, Pages, Des, Rating, haveRead);
                    EditWindow editWindow = new EditWindow();
                    editWindow.Show();
                    this.Close();
            }
        }
       //Allows enter button to be hit to edit the book
        private void EnterClick(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Edit_Button_Click(sender, e);
            }
        }
    }
}
