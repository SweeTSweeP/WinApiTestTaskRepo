namespace LibraryManager.LibraryInfrastructure.LibrarianDir
{
    internal class Librarian
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> OverdueBooks { get; } = new List<int>();

        public Librarian(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
