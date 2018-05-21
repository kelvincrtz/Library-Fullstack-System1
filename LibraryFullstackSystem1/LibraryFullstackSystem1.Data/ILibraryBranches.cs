using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data
{
    public interface ILibraryBranches
    {
        IEnumerable<LibraryBranch> GetAll();
        LibraryBranch GetById(int id);
    }
}
