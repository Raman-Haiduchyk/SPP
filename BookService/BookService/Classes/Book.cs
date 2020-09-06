using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Classes
{
    public class Book: IEquatable<Book>, IComparable<Book>, IComparable
    {
        #region Fields
        /// <summary>
        ///  ISBN identifier
        /// </summary>
        private string isbn;

        /// <summary>
        ///  Author name
        /// </summary>
        private string author;

        /// <summary>
        ///  Book title
        /// </summary>
        private string title;

        /// <summary>
        ///  Publishing house
        /// </summary>
        private string publisher;

        /// <summary>
        ///  Count of pages
        /// </summary>
        private int pagesCount;

        /// <summary>
        ///  Book price
        /// </summary>
        private double price;

        /// <summary>
        ///  Year of book publishing
        /// </summary>
        private int publishedAt;
        #endregion

        #region Properties
        public string ISBN
        {
            get => isbn;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {

                }
                isbn = value;
            }
        }

        public string Author
        {
            get => author;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {

                }
                author = value;
            }
        }

        public string Title
        {
            get => title;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {

                }
                title = value;
            }
        }

        public string Publisher
        {
            get => publisher;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {

                }
                publisher = value;
            }
        }

        public int PublishedAt
        {
            get => publishedAt;
            private set
            {
                if (value < 1 || value > DateTime.Today.Year)
                {

                }
                publishedAt = value;
            }
        }

        public int PagesCount
        {
            get => pagesCount;
            private set
            {
                if (value < 1)
                {

                }
                pagesCount = value;
            }
        }

        public double Price
        {
            get => price;
            private set
            {
                if (value < 0 + 0.0001)
                {

                }
                price = value;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates book exemplar.
        /// </summary>
        /// <param name="isbn">ISBN identifier</param>
        /// <param name="author">Athor's name.</param>
        /// <param name="title">Book title.</param>       
        /// <param name="publisher">Publishing house.</param>
        /// <param name="publishedAt">Year of book publishing.</param>
        /// <param name="pagesCount">Count of book pages.</param>
        /// <param name="price">Book price.</param>
        public Book(string isbn, string author, string title, string publisher, int publishedAt, int pagesCount, double price)
        {
            ISBN = isbn;
            Author = author;
            Title = title;
            Publisher = publisher;
            PublishedAt = publishedAt;
            PagesCount = pagesCount;
            Price = price;
        }
        #endregion

        #region System.Object overridden methods

        public override string ToString() => $"ISBN: {ISBN}, Author: {Author}, Title: {Title}, Publishing house: {Publisher}, Year: {PublishedAt}, Count of pages: {PagesCount}, Price: {Price}";

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) ? true : Equals(obj as Book);
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + ISBN.GetHashCode();
                hash = hash * 23 + Author.GetHashCode();
                hash = hash * 23 + Title.GetHashCode();
                hash = hash * 23 + Publisher.GetHashCode();
                hash = hash * 23 + PublishedAt.GetHashCode();
                hash = hash * 23 + PagesCount.GetHashCode();
                hash = hash * 23 + Price.GetHashCode();
                return hash;
            }
        }
        #endregion

        #region IEquatable implementation
        public bool Equals(Book book)
        {
            if (book == null) return false;
            return (ISBN == book.ISBN) &&
                   (Author == book.Author) &&
                   (Title == book.Title) &&
                   (Publisher == book.Publisher) &&
                   (PublishedAt == book.PublishedAt) &&
                   (PagesCount == book.PagesCount) &&
                   (Price == book.Price);
        }

        #endregion

        #region IComparable implementation

        public int CompareTo(object obj)
        {
            if (Equals(obj)) return 0;
            return CompareTo(obj as Book);
        }

        public int CompareTo(Book other)
        {
            if (other == null) return 1;
            return Title.CompareTo(other.Title); 
        }

        #endregion

    }
}
