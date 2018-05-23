using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryFullstackSystem1.Services
{
    public class AssetService : ILibraryAsset
    {
        private readonly LibrarySystemDbContext _DbContext;

        public AssetService(LibrarySystemDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public void Add(LibraryAsset newAsset)
        {
            _DbContext.Add(newAsset);
            _DbContext.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _DbContext.LibraryAssets
                .Include(p => p.Status)
                .Include(p => p.Location);
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _DbContext.LibraryAssets.OfType<Book>().Where(p => p.Id == id).Any();
            var isVideo = _DbContext.LibraryAssets.OfType<Video>().Where(p => p.Id == id).Any();

            return isBook ?
                _DbContext.Books.FirstOrDefault(p => p.Id == id).Author :
                _DbContext.Videos.FirstOrDefault(p => p.Id == id).Director
                ?? "Unknown";
        }

        public LibraryAsset GetById(int id)
        {
            return _DbContext.LibraryAssets
                .Include(p => p.Status)
                .Include(p => p.Location)
                .FirstOrDefault(p => p.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return _DbContext.LibraryAssets
                .FirstOrDefault(i => i.Id == id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_DbContext.Books.Any(p => p.Id == id))
            {
                return _DbContext.Books.FirstOrDefault(p => p.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_DbContext.Books.Any(p => p.Id == id))
            {
                return _DbContext.Books.FirstOrDefault(p => p.Id == id).ISBN;
            }
            else return "";
        }

        public string GetTitle(int id)
        {
            return _DbContext.LibraryAssets.FirstOrDefault(p => p.Id == id).Title;
        }

        public string GetType(int id)
        {
            var isBook = _DbContext.LibraryAssets.OfType<Book>().Where(p =>p.Id == id).Any();
            var isVideo = _DbContext.LibraryAssets.OfType<Video>().Where(p => p.Id == id).Any();

            return isBook ?
                "Book" : "Video";
        }

        public IEnumerable<LibraryAsset> SearchByTitle(string title)
        {
            return _DbContext.LibraryAssets.Where(p => p.Title == title);
        }
    }
}
