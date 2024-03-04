using LibraryManager.LibraryInfrastructure.ReaderDir;
using Newtonsoft.Json;
using System.IO;

namespace LibraryManager.LibraryInfrastructure.LibrarianDir
{
    internal class LibrarianRepository
    {
        private List<Librarian> librarians;
        private string fileName = "Librarians.json";

        public List<Librarian> GetLibrarians()
        {
            if (librarians != null) return librarians;

            if (!File.Exists(fileName)) return SaveRawData();

            using StreamReader re = new(fileName);
            using JsonTextReader reader = new(re);

            var serializer = new JsonSerializer();

            librarians = serializer.Deserialize<List<Librarian>>(reader)!;

            if (librarians != null)
                return librarians;
            else
                return SaveRawData();
        }

        public void SaveLibrarians(List<Librarian> librarians)
        {
            using StreamWriter wr = new(fileName);
            using JsonTextWriter writer = new(wr);

            var serializer = new JsonSerializer();

            serializer.Serialize(writer, librarians);
        }

        private List<Librarian> SaveRawData()
        {
            librarians = new List<Librarian>();
            SaveLibrarians(librarians);

            return librarians;
        }
    }
}
