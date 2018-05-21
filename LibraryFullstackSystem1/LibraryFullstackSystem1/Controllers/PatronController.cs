using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                LibrayCardId = result.LibraryCard.Id
            });

            var model = new PatronIndexModel()
            {
                Patrons = listingResult
            };

            return View(model);
        }
    }
}