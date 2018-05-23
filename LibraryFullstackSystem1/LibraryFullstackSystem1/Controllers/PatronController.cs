using System.Linq;
using LibraryFullstackSystem1.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryFullstackSystem1.Models.Patron;

namespace LibraryFullstackSystem1.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatron _Patron;
        private readonly IHold _Hold;
        private readonly ILibraryAsset _Asset;

        public PatronController(IPatron Patron, IHold Hold, ILibraryAsset Asset)
        {
            _Patron = Patron;
            _Hold = Hold;
            _Asset = Asset;
        }

        public IActionResult Index()
        {
            var patronsModel = _Patron.GetAll();

            var listingResult = patronsModel.Select(result => new PatronsIndexListingModel()
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                LibrayCardId = result.LibraryCard.Id,
                HomebranchLocation = result.HomeLibraryBranch.Name
            });

            var model = new PatronIndexModel()
            {
                Patrons = listingResult
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var patron = _Patron.GetById(id);

            var currentPatronHold = _Hold.GetByLibraryPatronId(patron.LibraryCard.Id)
                .Select(result => new PatronHoldDetailModel{
                    AssetId = result.LibraryAsset.Id,
                    HoldPlaced = result.HoldPlaced,
                    AssetTitle = _Asset.GetTitle(result.LibraryAsset.Id)
                });

            var model = new PatronDetailModel()
            {
                Id = patron.Id,
                LastName = patron.LastName,
                FirstName = patron.FirstName,
                Address = patron.Address,
                Telephone = patron.TelephoneNumber,
                Branch = patron.HomeLibraryBranch.Name,
                DateOfBirth = patron.DateOfBirth,
                Fee = patron.LibraryCard.Fees,
                PatronHolds = currentPatronHold
            };


            return View(model);
        }
    }
}