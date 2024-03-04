using LibraryManager.LibraryInfrastructure.BookDir;

namespace LibraryManager.LibraryInfrastructure.ObserverService
{
    public interface IBookObserver
    {
        void Update(int bookId, BookStatus status, DateTime reserveDate, DateTime returnDate);
    }
}
