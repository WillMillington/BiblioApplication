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
using BiblioBusiness;

namespace BiblioWPF
{
    /// <summary>
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {

        public EditWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Message.Text = $"This book has been updated in the library.";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EnterClick(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}