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

namespace BookService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private BookList bookList;

        public MainWindow()
        {
            InitializeComponent();
            bookList = new BookList();
            bookList.AddBook(new Book("dewdwe", "ferfe", "freferf", "freferf", 344, 34, 435.43));
            bookListView.ItemsSource = bookList.Books;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void delBtn_Click(object sender, RoutedEventArgs e)
        {
            if (bookListView.SelectedIndex != -1) bookList.DeleteBook(bookListView.SelectedIndex);
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loadBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addFromFileBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveAsBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == false) return;
            string filepath = saveFileDialog.FileName;
            bookList.Storage = new BookStorage(filepath);
            bookList.SaveList();
        }
    }
}
