using System;
using System.Collections.Generic;
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
        private BiblioManager _biblioManager = new BiblioManager();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void PopulateItemsControl()
        {
            BookBox.ItemsSource = _biblioManager.GetAllBooksByTitle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBookWindow = new AddBook();
            addBookWindow.Show();
        }

        private void PopulateItemsControlBox(object sender, EventArgs e)
        {
            PopulateItemsControl();
        }

        private void BookPage(object sender, MouseButtonEventArgs e)
        {
            BookPage bookPage = new BookPage();
            bookPage.
        }
    }
}