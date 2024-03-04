using Newtonsoft.Json;
using System.IO;

namespace LibraryManager.LibraryInfrastructure.ReaderDir
{
    internal class ReaderRepository
    {
        private List<Reader> readers;
        private string fileName = "Readers.json";

        public List<Reader> GetBooks()
        {
            if (readers != null) return readers;

            if (!File.Exists(fileName)) return SaveRawData();

            using StreamReader re = new(fileName);
            using JsonTextReader reader = new(re);

            var serializer = new JsonSerializer();

            readers = serializer.Deserialize<List<Reader>>(reader)!;

            if (readers != null)
                return readers;
            else
                return SaveRawData();
        }

        public void SaveReaders(List<Reader> readers)
        {
            using StreamWriter wr = new(fileName);
            using JsonTextWriter writer = new(wr);

            var serializer = new JsonSerializer();

            serializer.Serialize(writer, readers);
        }

        private List<Reader> SaveRawData()
        {
            readers = new List<Reader>();
            SaveReaders(readers);

            return readers;
        }
    }
}
