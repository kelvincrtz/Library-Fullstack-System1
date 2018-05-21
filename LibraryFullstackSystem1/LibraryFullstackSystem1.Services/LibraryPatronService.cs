using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace LibraryFullstackSystem1.Services
{
    public class LibraryPatronService : IPatron
    {
        private readonly LibrarySystemDbContext _DbContext;

        public LibraryPatronService(LibrarySystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public IEnumerable<Patron> GetAll()
        {
            return _DbContext.Patrons;
        }

        public Patron GetById(int id)
        {
            return _DbContext.Patrons.FirstOrDefault(p => p.Id == id);
        }
    }
}
