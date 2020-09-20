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

        #region Fields

        private BookList bookList;

        private string currentSortTag = null;

        private ListSortDirection currentSortDirection = ListSortDirection.Descending;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            bookList = new BookList();
            bookList.AddBook(new Book("111-222-333-4444", "ferfe", "freferf", "freferf", 344, 34, 43543));
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
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.ParamName);
            }
            catch
            {
                MessageBox.Show("Wrong data");
            }
            return;
        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bookListView.SelectedIndex != -1) bookList.DeleteBook(bookListView.SelectedIndex);
            }
            catch
            {
                MessageBox.Show("Delete error occured.");
            }
            
        }

        #endregion

        #region Save and Load methods

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == false) return;
                string filepath = saveFileDialog.FileName;
                bookList.Storage = new BookStorage(filepath);
                bookList.SaveList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == false) return;
                string filepath = openFileDialog.FileName;
                bookList.Storage = new BookStorage(filepath);
                bookList.LoadNewList();
                bookListView.ItemsSource = bookList.Books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void addFromFileBtn_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == false) return;
                string filepath = openFileDialog.FileName;
                bookList.LoadListToCurrent(new BookStorage(filepath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Sort methods

        private void SortISBN_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new ISBNComparer());
        }

        private void SortAuthor_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new AuthorComparer());
        }

        private void SortTitle_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new TitleComparer());
        }

        private void SortPublisher_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new PublisherComparer());
        }

        private void SortPublishedAt_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new PublishedAtCountComparer());
        }

        private void SortPages_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new PagesCountComparer());
        }

        private void SortPrice_Click(object sender, RoutedEventArgs e)
        {
            bookList.Sort(new PriceComparer());
        }

        #endregion

        #region Input processing

        private void intTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.Back)) e.Handled = true;
        }

        private void isbnTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key == Key.OemMinus || e.Key == Key.Back)) e.Handled = true;
        }

        #endregion

    }
}
