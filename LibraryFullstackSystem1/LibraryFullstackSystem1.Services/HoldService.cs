using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryFullstackSystem1.Services
{
    public class HoldService : IHold
    {
        private readonly LibrarySystemDbContext _DbContext;

        public HoldService(LibrarySystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public IEnumerable<Hold> GetAll()
        {
            return _DbContext.Holds;
        }

        public Hold GetById(int id)
        {
            return _DbContext.Holds.FirstOrDefault(p=>p.Id == id);
        }

        public IEnumerable<Hold> GetByLibraryPatronId(int libraryId)
        {
            return _DbContext.Holds
                .Include(p=>p.LibraryCard)
                .Include(p=>p.LibraryAsset)
                .Where(p => p.LibraryCard.Id == libraryId);
        }
    }
}
