using LibraryManager.LibraryInfrastructure.LibrarianDir;
using LibraryManager.LibraryInfrastructure.ObserverService;
using LibraryManager.LibraryInfrastructure.ReaderDir;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class BookController
    {
        private BookRepository bookRepository;
        private ReaderRepository readerRepository;
        private LibrarianRepository librarianRepository;
        private BookFilterContext filterContext;
        private List<Book> books;
        private List<Reader> readers;
        private List<Librarian> librarians;

        public DataGrid bookDataGrid { get; set; }

        public BookController() 
        {
            bookRepository = new BookRepository();
            readerRepository = new ReaderRepository();
            librarianRepository = new LibrarianRepository();
            
            books = bookRepository.GetBooks();
            readers = readerRepository.GetBooks();
            librarians = librarianRepository.GetLibrarians();

            filterContext = new BookFilterContext();
            filterContext.books = books;

            SubscribeBooksNotifications();
        }

        public void InitBookData(BookFilterContext.FilterType filterType, string value)
        {
            bookDataGrid.Items.Clear();

            var filteredBooks = filterContext.FilterBooks(filterType, value);

            foreach (var book in filteredBooks)
            {
                var bookDTO = new BookViewDTO(book);
                bookDataGrid.Items.Add(bookDTO);
            }
        }

        public void ReserveBook(int bookId, int readerId, DateTime reserveDate, DateTime returnDate)
        {
            var book = books.FirstOrDefault(s => s.Id == bookId);

            if (book == null)
            {
                MessageBox.Show("No book with this ID");
                return;
            }

            BookObserveManager.NotifyObservers(bookId, BookStatus.Reserved, reserveDate, returnDate);
            
            var reader = readers.FirstOrDefault(s => s.Id == readerId);

            if (reader == null)
                reader = new Reader(readers.Count + 1);

            readers.Add(reader);
            reader.BooksOnHands.Add(bookId);

            bookRepository.SaveBooks(books);
            readerRepository.SaveReaders(readers);

            InitBookData(BookFilterContext.FilterType.All, string.Empty);
        }

        public void ReturnBook(int bookId, int readerId, int librarianId, string librarianName)
        {
            var book = books.FirstOrDefault(s => s.Id == bookId);

            if (book == null)
            {
                MessageBox.Show("No book with this ID");
                return;
            }

            var librarian = librarians.FirstOrDefault(s => s.Id == librarianId && s.Name == librarianName);
            var librarianById = librarians.FirstOrDefault(s => s.Id == librarianId);

            if (librarian == null && librarianById != null)
            {
                MessageBox.Show("Wrong librarian name");
                return;
            }

            var reader = readers.FirstOrDefault(s => s.Id == readerId);

            if (reader == null)
                reader = new Reader(readers.Count + 1);

            if (!reader.BooksOnHands.Contains(bookId))
            {
                MessageBox.Show("This reader did not reserve this book");
                readerRepository.SaveReaders(readers);
                return;
            }
            
            readers.Add(reader);
            reader.BooksOnHands.Remove(bookId);

            if (librarian == null && librarianById == null)
                librarian = new Librarian(librarianId, librarianName);

            librarians.Add(librarian);

            if (DateTime.Now > book.ReturnDate)
                librarian.OverdueBooks.Add(bookId);


            BookObserveManager.NotifyObservers(bookId, BookStatus.Available, default, default);

            readerRepository.SaveReaders(readers);
            bookRepository.SaveBooks(books);
            librarianRepository.SaveLibrarians(librarians);

            InitBookData(BookFilterContext.FilterType.All, string.Empty);
        }

        private void SubscribeBooksNotifications()
        {
            foreach (var book in books)
            {
                BookObserveManager.RemoveObserver(book);
                BookObserveManager.AddObserver(book);
            }
        }
    }
}
