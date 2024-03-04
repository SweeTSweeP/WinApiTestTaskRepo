using LibraryManager.LibraryInfrastructure.BookDir;

namespace LibraryManager.LibraryInfrastructure.ObserverService
{
    public class BookObserveManager
    {
        private static List<IBookObserver> observers = new List<IBookObserver>();

        public static void AddObserver(IBookObserver observer) => 
            observers.Add(observer);

        public static void RemoveObserver(IBookObserver observer) => 
            observers.Remove(observer);

        public static void NotifyObservers(int bookId, BookStatus status, DateTime reserveDate, DateTime returnDate)
        {
            foreach (var observer in observers)
            {
                observer.Update(bookId, status, reserveDate, returnDate);
            }
        }
    }
}
