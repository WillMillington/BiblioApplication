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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _orderChoice = "Book";
        private BiblioManager _biblioManager = new BiblioManager();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBookWindow = new AddBook();
            addBookWindow.Show();
            SearchBar.Text = "Search";
        }
        private void PopulateItemsSource(object sender, EventArgs e)
        {
            PopulateItems(_orderChoice);
            SearchBar.Text = "Search";
        }

        private void BookPage(object sender, MouseButtonEventArgs e)
        {
            var result = BookBox.SelectedItem;
            Frame.NavigationService.Navigate(new BookPage(result));
        }

        private void Book_Title_Clck(object sender, RoutedEventArgs e)
        {
            PopulateItems("Book");
            _orderChoice = "Book";
            SearchBar.Text = "Search";
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems("Author");
            _orderChoice = "Author";
            SearchBar.Text = "Search";
        }

        private void Added_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems("Added");
            _orderChoice = "Added";
            SearchBar.Text = "Search";
        }

        private void RemoveSearch(object sender, RoutedEventArgs e)
        {
            SearchBar.Text = "";
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            BookBox.ItemsSource =_biblioManager.SearchBooks(SearchBar.Text);
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            PopulateItems(_orderChoice);
            SearchBar.Text = "Search";
        }

        private void EnterHit(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && SearchBar.Text != "Search")
            {
                BookBox.ItemsSource = _biblioManager.SearchBooks(SearchBar.Text);
            }
        }
    }
}