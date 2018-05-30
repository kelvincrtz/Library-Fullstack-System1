using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data
{
    public interface IPatron
    {
        IEnumerable<Patron> GetAll();
        Patron GetById(int id);
        Patron GetNameByLibraryId(int libraryId);
    }
}
