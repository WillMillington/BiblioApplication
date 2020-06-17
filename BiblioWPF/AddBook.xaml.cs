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

namespace BiblioWPF
{
    /// <summary>
    /// Interaction logic for AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        bool haveRead = false;
        BiblioManager _biblioManager = new BiblioManager();
        public delegate void DataChangedEventHandler(object sender, EventArgs e);
        public event DataChangedEventHandler DataChanged;
        public AddBook()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = (regex.IsMatch(e.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Read_Input(object sender, RoutedEventArgs e)
        {
            bool haveRead = true;
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            _biblioManager.AddBook(AuthorFirst.Text, AuthorLast.Text, BookTitle.Text, ISBN10.Text, ISBN13.Text, Publisher.Text, PublishDate.Text, int.Parse(NumOfPages.Text), Description.Text, int.Parse(Review.Text), haveRead);
            DataChangedEventHandler handler = DataChanged;

            if (handler != null)
            {
                handler(this, new EventArgs());
            }
            this.Close();
        }
    }
}
