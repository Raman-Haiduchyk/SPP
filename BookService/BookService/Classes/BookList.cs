using System;
using System.Collections.Generic;
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
        public List<Book> Books { get; private set;}

        public BookStorage Storage 
        {
            private get => storage;
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
            Books = new List<Book>();
        }

        public BookList(BookStorage bookStorage)
        {
            if (storage == null) throw new ArgumentException();
            Storage = bookStorage;
            Books = new List<Book>(Storage.LoadBookList());
        }

        #endregion

        #region Work with storage methods
        public void SaveList()
        {
            if (Storage != null) Storage.SaveBookList(Books);
        }

        public void LoadNewList()
        {
            if (Storage != null) Books = new List<Book>(Storage.LoadBookList());
        }

        public void LoadListToCurrent()
        {
            if (Storage != null)
            {
                Books.AddRange(Storage.LoadBookList());
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
            if(index < 0 || index >= Books.Count) throw new ArgumentException();
            Books.RemoveAt(index);
        }

        public void Sort(IComparer<Book> comparer)
        {
            Books.Sort(comparer);
        }

        #endregion
    }
}
