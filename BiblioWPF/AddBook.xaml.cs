using System;
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
    public partial class AddBook : Window
    {
        bool haveRead = false;
        BiblioManager _biblioManager = new BiblioManager();
        public AddBook()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = (regex.IsMatch(e.Text));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("All data in this form will be lost. Are you sure you want to leave this page?","Bibliocentric",MessageBoxButton.YesNo);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
            
        }
        private void Read_Input(object sender, RoutedEventArgs e)
        {
           haveRead = true;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string I10 = (string.IsNullOrEmpty(ISBN10.Text))? null : ISBN10.Text ;
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
                _biblioManager.AddBook(AuthorFirst.Text, AuthorLast.Text, BookTitle.Text, I10, I13, Pub, PubDate, Pages, Des, Rating, haveRead);
                //Window window = MainWindow.GetWindow(MainWindow);
                //var mainWindow = (MainWindow)window;
                //mainWindow.PopulateItemsControl();
                MessageBox.Show("Book Added");
                this.Close();
            }
        }
        private void EnterClick(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Add_Button_Click(sender, e);
            }
        }
    }
}
