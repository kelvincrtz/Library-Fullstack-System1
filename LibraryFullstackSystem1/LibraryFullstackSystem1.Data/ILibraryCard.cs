using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data
{
    public interface ILibraryCard
    {
        LibraryCard GetById(int id);
    }
}
