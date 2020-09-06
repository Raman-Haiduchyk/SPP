using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Classes
{
    class BookList
    {

        #region Fields

        private BookStorage storage;

        #endregion

        #region Properties
        public ObservableCollection<Book> Books { get; private set; }

        public BookStorage Storage
        {
            get => storage;
            set
            {
                if (value == null) throw new ArgumentException();
                storage = value;
            }
        }

        #endregion

        #region Constructor

        public BookList()
        {
            Books = new ObservableCollection<Book>();
        }

        public BookList(BookStorage bookStorage)
        {
            if (storage == null) throw new ArgumentException();
            Storage = bookStorage;
            Books = new ObservableCollection<Book>(Storage.LoadBookList());
        }

        #endregion

        #region Work with storage methods
        public void SaveList()
        {
            if (Storage != null) Storage.SaveBookList(Books);
        }

        public void LoadNewList()
        {
            if (Storage != null) Books = new ObservableCollection<Book>(Storage.LoadBookList());
        }

        public void LoadListToCurrent(BookStorage bookStorage)
        {
            if (bookStorage != null)
            {
                foreach (Book book in bookStorage.LoadBookList()) Books.Add(book);
            }
        }

        #endregion

        #region Work with list methods

        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentException();
            Books.Add(book);
        }

        public void DeleteBook(int index)
        {
            if (index < 0 || index >= Books.Count) throw new ArgumentException();
            Books.RemoveAt(index);
        }

        public void Sort(IComparer<Book> comparer)
        {
            List<Book> bufList = new List<Book>(Books);
            bufList.Sort(comparer);
            Books = new ObservableCollection<Book>(bufList);
        }

        #endregion
    }

    #region Comparers

    public class ISBNComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return string.Compare(x.ISBN, y.ISBN);
        }
    }

    public class AuthorComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return string.Compare(x.Author, y.Author);
        }
    }

    public class TitleComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return string.Compare(x.Title, y.Title);
        }
    }

    public class PublisherComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return string.Compare(x.Publisher, y.Publisher);
        }
    }

    public class PublishedAtCountComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.PublishedAt - y.PublishedAt;
        }
    }

    public class PagesCountComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.PagesCount - y.PagesCount;
        }
    }

    public class PriceComparer : IComparer<Book>
    {
        public int Compare(Book x, Book y)
        {
            return x.Price - y.Price;
        }
    }

    #endregion

}
