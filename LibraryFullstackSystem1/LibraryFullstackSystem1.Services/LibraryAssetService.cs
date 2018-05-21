using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryFullstackSystem1.Services
{
    public class LibraryAssetService : ILibraryAsset
    {
        private readonly LibrarySystemDbContext _LibradyFullstackSystemDbContext;

        public LibraryAssetService(LibrarySystemDbContext LibradyFullstackSystemDbContext)
        {
            _LibradyFullstackSystemDbContext = LibradyFullstackSystemDbContext;
        }

        public void Add(LibraryAsset newAsset)
        {
            _LibradyFullstackSystemDbContext.Add(newAsset);
            _LibradyFullstackSystemDbContext.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _LibradyFullstackSystemDbContext.LibraryAssets
                .Include(p => p.Status)
                .Include(p => p.Location);
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _LibradyFullstackSystemDbContext.LibraryAssets.OfType<Book>().Where(p => p.Id == id).Any();
            var isVideo = _LibradyFullstackSystemDbContext.LibraryAssets.OfType<Video>().Where(p => p.Id == id).Any();

            return isBook ?
                _LibradyFullstackSystemDbContext.Books.FirstOrDefault(p => p.Id == id).Author :
                _LibradyFullstackSystemDbContext.Videos.FirstOrDefault(p => p.Id == id).Director
                ?? "Unknown";
        }

        public LibraryAsset GetById(int id)
        {
            return _LibradyFullstackSystemDbContext.LibraryAssets
                .Include(p => p.Status)
                .Include(p => p.Location)
                .FirstOrDefault(p => p.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return _LibradyFullstackSystemDbContext.LibraryAssets
                .FirstOrDefault(i => i.Id == id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_LibradyFullstackSystemDbContext.Books.Any(p => p.Id == id))
            {
                return _LibradyFullstackSystemDbContext.Books.FirstOrDefault(p => p.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_LibradyFullstackSystemDbContext.Books.Any(p => p.Id == id))
            {
                return _LibradyFullstackSystemDbContext.Books.FirstOrDefault(p => p.Id == id).ISBN;
            }
            else return "";
        }

        public string GetTitle(int id)
        {
            return _LibradyFullstackSystemDbContext.LibraryAssets.FirstOrDefault(p => p.Id == id).Title;
        }

        public string GetType(int id)
        {
            var isBook = _LibradyFullstackSystemDbContext.LibraryAssets.OfType<Book>().Where(p =>p.Id == id).Any();
            var isVideo = _LibradyFullstackSystemDbContext.LibraryAssets.OfType<Video>().Where(p => p.Id == id).Any();

            return isBook ?
                "Book" : "Video";
        }
    }
}
