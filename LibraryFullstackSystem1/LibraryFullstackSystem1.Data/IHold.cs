using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data
{
    public interface IHold
    {
        IEnumerable<Hold> GetAll();
        Hold GetById(int id);
        IEnumerable<Hold> GetByLibraryPatronId(int id);
    }
}
