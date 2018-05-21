using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFullstackSystem1.Services
{
    public class LibraryBranchService : ILibraryBranch
    {
        private readonly LibrarySystemDbContext _DbContext;

        public LibraryBranchService(LibrarySystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _DbContext.LibraryBranches;
        }

        public LibraryBranch GetById(int id)
        {
            return _DbContext.LibraryBranches.FirstOrDefault(p => p.Id == id);
        }
    }
}
