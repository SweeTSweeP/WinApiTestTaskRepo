namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class BookFilterContext
    {
        public enum FilterType
        {
            All,
            ByTitle,
            ByAuthor,
            ByAlphabet
        }

        public List<Book> books { get; set; }

        public List<Book> FilterBooks(FilterType filterType, string value)
        {
            switch (filterType)
            {
                default: return All();
                case FilterType.All: return All();
                case FilterType.ByTitle: return ByTitle(value);
                case FilterType.ByAuthor: return ByAuthor(value);
                case FilterType.ByAlphabet: return ByAlphabet();
            }
        }

        private List<Book> All() =>
            books;

        private List<Book> ByTitle(string value) =>
            books.Where(s => s.Title == value).ToList();

        private List<Book> ByAuthor(string value) =>
            books.Where(s => s.Author == value).ToList();

        private List<Book> ByAlphabet() =>
            books.OrderBy(s=>s.Title).ToList();
    }
}
