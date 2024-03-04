using Newtonsoft.Json;
using System.IO;

namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class BookRepository
    {
        private List<Book> books;
        private string fileName = "Books.json";

        public List<Book> GetBooks()
        {
            if (books != null) return books;

            if (!File.Exists(fileName)) return SaveRawData();
            
            using StreamReader re = new(fileName);
            using JsonTextReader reader = new(re);

            var serializer = new JsonSerializer();

            books = serializer.Deserialize<List<Book>>(reader)!;
            
            if (books != null)
                return books;
            else
                return SaveRawData();

            //return books ?? SaveRawData();
        }

        public void SaveBooks(List<Book> books)
        {
            using StreamWriter wr = new(fileName);
            using JsonTextWriter writer = new(wr);

            var serializer = new JsonSerializer();

            serializer.Serialize(writer, books);
        }

        private List<Book> SaveRawData()
        {
            books = BooksRawData;
            SaveBooks(books);

            return books;
        }

        private List<Book> BooksRawData = new()
        {
            new() 
            {
                Id = 1,
                Title = "Alice's Adventures in Wonderland",
                Author = "Lewis Carroll",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new() 
            {
                Id = 2,
                Title = "The Old Man and the Sea",
                Author = "Ernest Hemingway",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new()
            {
                Id = 3,
                Title = "The Three Musketeers",
                Author = "Alexandre Dumas",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new() 
            {
                Id = 4,
                Title = "The Catcher in the Rye",
                Author = "J. D. Salingers",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new() 
            {
                Id = 5,
                Title = "The Lord of the Rings",
                Author = "J. R. R. Tolkien",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new() 
            {
                Id = 6,
                Title = "Dune",
                Author = "Frank Herbert",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
            new() 
            {
                Id = 7,
                Title = "The Picture of Dorian Gray",
                Author = "Oscar Wilde",
                ReservationDate = default,
                ReturnDate = default,
                Status = BookStatus.Available
            },
        };
    }
}
