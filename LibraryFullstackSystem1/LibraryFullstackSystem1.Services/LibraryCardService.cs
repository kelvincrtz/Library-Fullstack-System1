using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFullstackSystem1.Services
{
    public class LibraryCardService : ILibraryCard
    {
        private readonly LibrarySystemDbContext _DbContext;

        public LibraryCardService(LibrarySystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public LibraryCard GetById(int id)
        {
            return _DbContext.LibraryCards.Include(p => p.Checkouts).FirstOrDefault(p =>p.Id==id);
        }
    }
}
