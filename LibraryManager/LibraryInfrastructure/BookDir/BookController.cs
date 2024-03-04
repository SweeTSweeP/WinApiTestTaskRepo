using LibraryManager.LibraryInfrastructure.ObserverService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class BookController
    {
        private BookRepository bookRepository;
        private BookFilterContext filterContext;
        private List<Book> books;

        public DataGrid bookDataGrid { get; set; }

        public BookController() 
        {
            bookRepository = new BookRepository();
            books = bookRepository.GetBooks();

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

        public void ReserveBook(int id)
        {
            BookObserveManager.NotifyObservers(id, BookStatus.Reserved);
            bookRepository.SaveBooks(books);
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
