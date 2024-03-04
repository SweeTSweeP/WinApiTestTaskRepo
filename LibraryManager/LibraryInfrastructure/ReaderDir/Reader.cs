namespace LibraryManager.LibraryInfrastructure.ReaderDir
{
    public class Reader
    {
        public int Id { get; set; }
        
        public List<int> BooksOnHands { get; } = new List<int>();

        public Reader(int id) => 
            Id = id;
    }
}
