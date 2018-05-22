using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data
{
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);

        void Add(LibraryAsset newAsset);
        string GetAuthorOrDirector(int id);
        string GetDeweyIndex(int id);
        string GetTitle(int id);
        string GetType(int id);
        string GetIsbn(int id);

        LibraryBranch GetCurrentLocation(int id);

        IEnumerable<LibraryAsset> SearchByTitle(string title);
    }
}
