using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Classes
{
    class BookStorage
    {
        #region Fields

        private string path;

        #endregion

        #region Constructor

        public BookStorage(string filepath)
        {
            path = string.Copy(filepath) ?? throw new ArgumentNullException($"Path cannot be null.");
        }

        #endregion

        #region Public methods

        public void SaveBookList(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    SaveBook(book, writer);
                }
            }
        }

        public IEnumerable<Book> LoadBookList()
        {
            if (!File.Exists(path)) throw new ArgumentException("Incorrect file path.");
            
            var books = new List<Book>();

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    books.Add(LoadBook(reader));
                }
            }
            return books;
        }

        #endregion

        #region Private methods

        private void SaveBook (Book book, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(book.ISBN);
            binaryWriter.Write(book.Author);
            binaryWriter.Write(book.Title);
            binaryWriter.Write(book.Publisher);
            binaryWriter.Write(book.PublishedAt);
            binaryWriter.Write(book.PagesCount);
            binaryWriter.Write(book.Price);
        }

        private Book LoadBook(BinaryReader binaryReader)
        {
            string isbn = binaryReader.ReadString();
            string author = binaryReader.ReadString();
            string title = binaryReader.ReadString();
            string publisher = binaryReader.ReadString();
            int publishedAt = binaryReader.ReadInt32();
            int pagesCount = binaryReader.ReadInt32();
            int price = binaryReader.ReadInt32();
            return new Book(isbn, author, title, publisher, publishedAt, pagesCount, price);
        }

        #endregion
    }
}
