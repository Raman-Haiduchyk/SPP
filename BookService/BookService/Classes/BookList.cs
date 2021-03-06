﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

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
                if (value == null) throw new ArgumentException("Storage can't be null");
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
            try
            {
                Storage = bookStorage;
                Books = new ObservableCollection<Book>(Storage.LoadBookList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        #endregion

        #region Work with storage methods
        public void SaveList()
        {
            try
            {
                if (Storage != null) Storage.SaveBookList(Books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void LoadNewList()
        {
            try
            {
                if (Storage != null) Books = new ObservableCollection<Book>(Storage.LoadBookList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void LoadListToCurrent(BookStorage bookStorage)
        {
            try
            {
                if (bookStorage != null)
                {
                    foreach (Book book in bookStorage.LoadBookList()) Books.Add(book);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            Books.Clear();
            foreach (Book book in bufList) Books.Add(book);
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
            double a, b;
            double.TryParse(x.Price, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out a);
            double.TryParse(y.Price, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out b);
            return (a > b) ? 1 : -1;
        }
    }

    #endregion

}
