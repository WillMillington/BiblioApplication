using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BiblioBusiness;

namespace BiblioWPF
{
    public partial class MainWindow : Window
    {
        //Variable set to hold previous order of books
        private string _orderChoice = "Book";

        //Creates a new BiblioManager Object
        private BiblioManager _biblioManager = new BiblioManager();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //When this window is activated it calls the PopulateItems the list of books in the order of "_orderChoice"
        private void PopulateItemsSource(object sender, EventArgs e)
        {
            PopulateItems(_orderChoice);
            SearchBar.Text = "Search";
        }

        //Actual method to populate the the BookBox, uses switch to check input of method and order the books accordingly
        public void PopulateItems(string order)
        {
            Count.Text = $"Total library count: {_biblioManager.GetCount()}";
            switch(order)
            {
                case "Book":
                    BookBox.ItemsSource = _biblioManager.GetAllBooksByTitle();
                    MenuTitle.Header = "Order by: Book Title";
                    break;
                case "Author":
                    BookBox.ItemsSource = _biblioManager.GetAllBooksByAuthor();
                    MenuTitle.Header = "Order by: Book Author Surname";
                    break;
                case "Added":
                    BookBox.ItemsSource = _biblioManager.GetAllBooksByAdded();
                    MenuTitle.Header = "Order by: Book Date Added";
                    break;

            }
            
        }

        //Add a book button to call the Add book page (AddBook.xaml)
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBookWindow = new AddBook();
            addBookWindow.Show();
            SearchBar.Text = "Search";
        }

        //Sets selected item and opens a new book page (BookPage.xaml) that is populated with the seleceted book's information
        private void BookPage(object sender, MouseButtonEventArgs e)
        {
            var result = BookBox.SelectedItem;
            Frame.NavigationService.Navigate(new BookPage(result));
        }

        //Re-populates list in book title order
        private void Book_Title_Clck(object sender, RoutedEventArgs e)
        {
            PopulateItems("Book");
            _orderChoice = "Book";
            SearchBar.Text = "Search";
        }

        //Re-populates list in author surname order
        private void Author_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems("Author");
            _orderChoice = "Author";
            SearchBar.Text = "Search";
        }

        //Re-populates list in added order
        private void Added_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems("Added");
            _orderChoice = "Added";
            SearchBar.Text = "Search";
        }

        //When search bar is in focus the textbox becomes empty and removes "Search"
        private void RemoveSearch(object sender, RoutedEventArgs e)
        {
            SearchBar.Text = "";
        }

        //When search button is clicked the BiblioManager SearchBooks method is called handing in the textbox input
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            BookBox.ItemsSource =_biblioManager.SearchBooks(SearchBar.Text);
        }

        //Returns the user home with the book population ordered by choice
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems(_orderChoice);
            SearchBar.Text = "Search";
        }

        //When the enter is clicked, if the search bar is empty nothing happens, if it has something else in then the search function is called
        private void EnterHit(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && SearchBar.Text != "Search")
            {
                BookBox.ItemsSource = _biblioManager.SearchBooks(SearchBar.Text);
            }
        }
    }
}