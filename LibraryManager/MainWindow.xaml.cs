using LibraryManager.LibraryInfrastructure.BookDir;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using LibraryManager.LibraryInfrastructure.BookDir;
using System.Net;
using LibraryManager.LibraryInfrastructure.LibrarianDir;

namespace LibraryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookController bookController;
        private LibrarianController librarianController;

        public MainWindow()
        {
            InitializeComponent();
            bookController = new BookController();
            librarianController = new LibrarianController();
            bookController.bookDataGrid = bookData;
            bookController.InitBookData(BookFilterContext.FilterType.All, string.Empty);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {

            if (isTitle.IsChecked == true)
            {
                bookController.InitBookData(BookFilterContext.FilterType.ByTitle, searchField.Text);
            }
            else if (isAuthor.IsChecked == true)
            {
                bookController.InitBookData(BookFilterContext.FilterType.ByAuthor, searchField.Text);
            }
            else if (isAll.IsChecked == true)
            {
                bookController.InitBookData(BookFilterContext.FilterType.All, string.Empty);
            }
            else
            {
                bookController.InitBookData(BookFilterContext.FilterType.All, string.Empty);
            }
        }

        private void sortByAlphabetButton_Click(object sender, RoutedEventArgs e)
        {
            bookController.InitBookData(BookFilterContext.FilterType.ByAlphabet, string.Empty);
        }

        private void reserveBookButton_Click(object sender, RoutedEventArgs e)
        {
            var bookId = reserveBookBookIdField.Text;
            var readerId = reserveBookReaderIdField.Text;
            DateTime dateOfReservation = default;
            DateTime dateOfReturn = default;

            if (IsNotDigitOrEmpty(bookId, "Book ID")) return;

            if (IsNotDigitOrEmpty(readerId, "Reader ID")) return;

            if (!DateTime.TryParse(reservationDateField.Text, out dateOfReservation))
            {
                MessageBox.Show($"Wrong format, try {DateTime.Now.GetDateTimeFormats()[0]}");
                return;
            }

            if (!DateTime.TryParse(returnDateField.Text, out dateOfReturn))
            {
                MessageBox.Show($"Wrong format, try {DateTime.Now.GetDateTimeFormats()[0]}");
                return;
            }

            bookController.ReserveBook(int.Parse(bookId), int.Parse(readerId), dateOfReservation, dateOfReturn);
        }

        private void returnBookButton_Click(object sender, RoutedEventArgs e)
        {
            var librarianId = librarianIdFied.Text;
            var librarianName = librarianNameField.Text;
            var bookId = returnBookBookIdField.Text;
            var readerId = returnBookReaderIdField.Text; ;

            if (IsNotDigitOrEmpty(librarianId, "Librarian ID")) return;

            if (string.IsNullOrEmpty(librarianName))
            {
                MessageBox.Show("Input book ID");
                return;
            }

            if (IsNotDigitOrEmpty(bookId, "Book ID")) return;

            if (IsNotDigitOrEmpty(readerId, "Reader ID")) return;

            bookController.ReturnBook(int.Parse(bookId), int.Parse(readerId), int.Parse(librarianId), librarianName);
        }

       
        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            var librarianId = librarianIdFied.Text;
            var librarianName = librarianNameField.Text;

            if (IsNotDigitOrEmpty(librarianId, "Librarian ID")) return;

            if (string.IsNullOrEmpty(librarianName))
            {
                MessageBox.Show("Input librarian name");
                return;
            }

            librarianController.MakeReport(int.Parse(librarianId), librarianName);
        }

        private bool IsNotDigitOrEmpty(string value, string nameOfField)
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show($"Input {nameOfField}");
                return true;
            }

            foreach (var symbol in value)
                if (!char.IsDigit(symbol))
                {
                    MessageBox.Show("Only digits for IDs");
                    return true;
                }

            return false;
        }
    }
}