using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace LibraryFullstackSystem1.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryAsset _ILibraryAsset;

        public CatalogController(ILibraryAsset LibraryAsset)
        {
            _ILibraryAsset = LibraryAsset;
        }

        public IActionResult Index()
        {
            var assetModels = _ILibraryAsset.GetAll();

            var listingResult = assetModels.Select(result => new AssetIndexListingModel() {
                Id = result.Id,
                ImageUrl = result.ImageURL,
                AuthorOrDirector = _ILibraryAsset.GetAuthorOrDirector(result.Id),
                DeweyCallNumber = _ILibraryAsset.GetDeweyIndex(result.Id),
                Title = result.Title,
                Type = _ILibraryAsset.GetType(result.Id)
            });

            var model = new AssetIndexModel()
            {
                Assets = listingResult
            };

            return View(model);
        }
    }
}