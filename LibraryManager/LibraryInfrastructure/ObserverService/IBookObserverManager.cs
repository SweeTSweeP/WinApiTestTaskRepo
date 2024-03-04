namespace LibraryManager.LibraryInfrastructure.ObserverService
{
    internal interface IBookObserverManager
    {
        void AddObserver(IBookObserver observer);
        void RemoveObserver(IBookObserver observer);
        void NotifyObservers();
    }
}
