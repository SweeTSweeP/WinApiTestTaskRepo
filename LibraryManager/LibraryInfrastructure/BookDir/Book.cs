using LibraryManager.LibraryInfrastructure.ObserverService;
using System.Windows;

namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class Book : IBookObserver
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public BookStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public void Update(int bookId, BookStatus status, DateTime reserveDate, DateTime returnDate)
        {
            if (bookId == Id)
            {
                ReservationDate = reserveDate;
                ReturnDate = returnDate;
                Status = status;
                MessageBox.Show($"Book {Title} is {Status}");
            }
        }
    }
}