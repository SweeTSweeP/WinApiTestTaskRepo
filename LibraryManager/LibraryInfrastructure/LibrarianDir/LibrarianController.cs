using LibraryManager.LibraryInfrastructure.BookDir;
using Newtonsoft.Json;
using System.Windows;
using ChoETL;

namespace LibraryManager.LibraryInfrastructure.LibrarianDir
{
    public class LibrarianController
    {
        public void MakeReport(int librarianId, string librarianName)
        {
            var librarianRepository = new LibrarianRepository();
            var librarians = librarianRepository.GetLibrarians();

            var bookRepository = new BookRepository();
            var books = bookRepository.GetBooks();

            var librarianById = librarians.FirstOrDefault(s => s.Id == librarianId);

            if (librarianById == null)
            {
                MessageBox.Show("Wrong librarian Id");
                return;
            }

            var librarian = librarians.FirstOrDefault(s => s.Id == librarianId && s.Name == librarianName);

            if (librarian == null && librarianById != null)
            {
                MessageBox.Show("Wrong librarian name");
                return;
            }

            var overdueBooks = books.Where(s => librarian.OverdueBooks.Contains(s.Id)).ToList();
            var jsonData = JsonConvert.SerializeObject(overdueBooks);

            using (var parser = new ChoCSVWriter<Book>($"Id{librarianId}_{librarianName}_{DateTime.Now.ToString("MM-dd-yy")}_Overdue_Books.csv"))
            {
                parser.Write(overdueBooks);
            }

            librarian.OverdueBooks.Clear();
            librarianRepository.SaveLibrarians(librarians);
        }
    }
}
