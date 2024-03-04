using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.LibraryInfrastructure.Reader
{
    public class Reader
    {
        public int Id { get; set; }
        
        public List<int> BooksOnHands { get; } = new List<int>();
    }
}
