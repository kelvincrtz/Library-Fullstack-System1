using System.Linq;
using LibraryFullstackSystem1.Data;
using Microsoft.AspNetCore.Mvc;
using LibraryFullstackSystem1.Models.Patron;

namespace LibraryFullstackSystem1.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatron _Patron;

        public PatronController(IPatron Patron)
        {
            _Patron = Patron;
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

            var model = new PatronDetailModel()
            {
                Id = patron.Id,
                LastName = patron.LastName,
                FirstName = patron.FirstName,
                Address = patron.Address,
                Telephone = patron.TelephoneNumber,
                Branch = patron.HomeLibraryBranch.Name,
                DateOfBirth = patron.DateOfBirth,
                Fee = patron.LibraryCard.Fees
            };


            return View(model);
        }
    }
}