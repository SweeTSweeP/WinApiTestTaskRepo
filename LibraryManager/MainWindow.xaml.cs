using LibraryManager.LibraryInfrastructure.BookDir;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using LibraryManager.LibraryInfrastructure.BookDir;

namespace LibraryManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BookController bookController;

        public MainWindow()
        {
            InitializeComponent();
            bookController = new BookController();
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
    }
}