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
        private readonly ICheckout _ICheckout;

        public CatalogController(ILibraryAsset LibraryAsset, ICheckout ICheckout)
        {
            _ILibraryAsset = LibraryAsset;
            _ICheckout = ICheckout;
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

        public IActionResult Detail(int id)
        {
            var asset = _ILibraryAsset.GetById(id);

            var currentHolds = _ICheckout.GetCurrentHolds(id)
                .Select(result => new AssetHoldModel
                {
                    HoldPlaced = _ICheckout.GetCurrentHoldPlaced(result.Id).ToString("d"),
                    PatronName = _ICheckout.GetCurrentHoldPatronName(result.Id),
                });

            var model = new AssetDetailModel()
            {
                AssetId = id,
                Title = asset.Title,
                AuthorOrDirector = _ILibraryAsset.GetAuthorOrDirector(id),
                Type = _ILibraryAsset.GetType(id),
                Year = asset.Year,
                ISBN = _ILibraryAsset.GetIsbn(id),
                DeweyCallNumber = _ILibraryAsset.GetDeweyIndex(id),
                Status = asset.Status.Name,
                Cost = asset.Cost,
                CurrentLocation = _ILibraryAsset.GetCurrentLocation(id).Name,
                ImageUrl = asset.ImageURL,
                CheckoutHistory = _ICheckout.GetCheckoutHistories(id),
                LatestCheckout = _ICheckout.GetLatestCheckout(id),
                PatronName = _ICheckout.GetCurrentCheckoutPatron(id),
                CurrentHolds = currentHolds
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var asset = _ILibraryAsset.GetById(id);

            var model = new CheckoutModel
            {
                AsssetId = id,
                ImageUrl = asset.ImageURL,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _ICheckout.IsCheckedOut(id)
            };

            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = _ILibraryAsset.GetById(id);

            var model = new CheckoutModel
            {
                AsssetId = id,
                ImageUrl = asset.ImageURL,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _ICheckout.IsCheckedOut(id),
                HoldCount = _ICheckout.GetCurrentHolds(id).Count()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult PlaceCheckout(int id, int libraryCardId)
        {
            _ICheckout.CheckOutItem(id, libraryCardId);

            return RedirectToAction("Detail", new { id = id });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _ICheckout.PlaceHold(assetId, libraryCardId);

            return RedirectToAction("Detail", new { id = assetId });
        }

        public IActionResult MarkLost(int assetId)
        {
            _ICheckout.MarkLost(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        public IActionResult MarkFound(int assetId)
        {
            _ICheckout.MarkFound(assetId);
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}