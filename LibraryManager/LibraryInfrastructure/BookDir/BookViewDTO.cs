namespace LibraryManager.LibraryInfrastructure.BookDir
{
    public class BookViewDTO
    {
        public string BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookStatus { get; set; }
        public string ReservationDate { get; set; }
        public string ReturnDate { get; set; }

        public BookViewDTO(Book book)
        {
            BookID = book.Id.ToString();
            BookTitle = book.Title;
            BookAuthor = book.Author;
            BookStatus = book.Status.ToString();
            ReservationDate = book.ReservationDate.ToString();
            ReturnDate = book.ReturnDate.ToString();
        }
    }
}
