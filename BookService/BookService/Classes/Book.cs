using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;

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
            set
            {
                var regex = new Regex(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$");

                if (string.IsNullOrEmpty(value) || !regex.IsMatch(value))
                {
                    throw new ArgumentNullException("Invalid ISBN.");
                }

                isbn = value;
            }
        }

        public string Author
        {
            get => author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Author string cannot be empty or null.");
                }
                author = value;
            }
        }

        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title string cannot be empty or null.");
                }
                title = value;
            }
        }

        public string Publisher
        {
            get => publisher;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Publisher string cannot be empty or null.");
                }
                publisher = value;
            }
        }

        public int PublishedAt
        {
            get => publishedAt;
            set
            {
                if (value < 1 || value > DateTime.Today.Year)
                {
                    throw new ArgumentOutOfRangeException("Publishing year cannot be less than zero or more than current year.");
                }
                publishedAt = value;
            }
        }

        public int PagesCount
        {
            get => pagesCount;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Pages count cannot be less than zero or equal to zero.");
                }
                pagesCount = value;
            }
        }

        public string Price
        {
            get => price.ToString("C", CultureInfo.CurrentCulture);
            set
            {
                double buf;
                if (double.TryParse(value, out buf))
                {

                    if (buf < 0.00001)
                    {
                        throw new ArgumentOutOfRangeException("Price cannot be so small.");
                    }
                    price = buf;
                }
                else
                {
                    throw new ArgumentException("Wrong price format.");
                }
                
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates book exemplar.
        /// </summary>
        /// <param name="isbn">ISBN identifier.</param>
        /// <param name="author">Athor's name.</param>
        /// <param name="title">Book title.</param>       
        /// <param name="publisher">Publishing house.</param>
        /// <param name="publishedAt">Year of book publishing.</param>
        /// <param name="pagesCount">Count of book pages.</param>
        /// <param name="price">Book price.</param>
        public Book(string isbn, string author, string title, string publisher, int publishedAt, int pagesCount, string price)
        {
           // try
            {
                ISBN = isbn;
                Author = author;
                Title = title;
                Publisher = publisher;
                PublishedAt = publishedAt;
                PagesCount = pagesCount;
                Price = price;
            }
        /*    catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentOutOfRangeException(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentNullException(ex.Message);
            }
        */
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
            if (ReferenceEquals(book, null)) return false;
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
            if (ReferenceEquals(other, null)) return 1;
            return Title.CompareTo(other.Title); 
        }

        #endregion

    }
}
