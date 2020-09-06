using System;
using System.Collections.Generic;
using System.Linq;
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
using BookService.Classes;
using Microsoft.Win32;
using System.ComponentModel;

namespace BookService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BookList bookList;

        private GridViewColumnHeader listViewSortCol = null;

        private ListSortDirection currentDirection = ListSortDirection.Descending;

        public MainWindow()
        {
            InitializeComponent();
            bookList = new BookList();
            bookList.AddBook(new Book("dewdwe", "ferfe", "freferf", "freferf", 344, 34, 43543));
            bookListView.ItemsSource = bookList.Books;
        }

        #region Add and Delete methods

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string isbn = isbnTextBox.Text;
                string author = authorTextBox.Text;
                string title = titleTextBox.Text;
                string publisher = publisherTextBox.Text;
                int publishedAt = int.Parse(publishedAtTextBox.Text);
                int pagesCount = int.Parse(pagesCountTextBox.Text);
                int price = int.Parse(priceTextBox.Text);
                bookList.AddBook(new Book(isbn, author, title, publisher, publishedAt, pagesCount, price));
            }
            catch
            {
                return;
            }
            
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookListView.SelectedIndex != -1) bookList.DeleteBook(bookListView.SelectedIndex);
        }

        #endregion

        #region Save and Load methods

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookList.Storage != null)
            {
                bookList.SaveList();
            }
            else
            {
                saveAsBtn_Click(sender, e);
            }
        }

        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == false) return;
            string filepath = saveFileDialog.FileName;
            bookList.Storage = new BookStorage(filepath);
            bookList.SaveList();
        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false) return;
            string filepath = openFileDialog.FileName;
            bookList.Storage = new BookStorage(filepath);
            bookList.LoadNewList();
            bookListView.ItemsSource = bookList.Books;
        }

        private void addFromFileBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false) return;
            string filepath = openFileDialog.FileName;
            bookList.LoadListToCurrent(new BookStorage(filepath));
        }

        #endregion


        #region Sort method

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                bookListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && currentDirection == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            currentDirection = newDir;
            bookListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        #endregion


    }
}
