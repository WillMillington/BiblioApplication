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
using BiblioBusiness;

namespace BiblioWPF
{
    public partial class DeleteWindow : Window
    {
        private BiblioManager _bibMan = new BiblioManager();
        public DeleteWindow(object selectedBook)
        {
            InitializeComponent();
            _bibMan.SetSelectedBook(selectedBook);
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Message.Text = $"This book will be removed from the library.{Environment.NewLine} Do you want to delete '{_bibMan.SelectedBook.Title}'?";
        }

        //Deletes book
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _bibMan.DeleteBook(_bibMan.SelectedBook);
            this.Close();
        }

        //Cancels delete
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Enter key click to delete
        private void EnterClick(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(sender, e);
            }
        }
    }
}
