using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Models.Branch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryFullstackSystem1.Controllers
{
    public class BranchController : Controller
    {
        private readonly ILibraryBranch _ILibraryBranches;

        public BranchController(ILibraryBranch ILibraryBranches)
        {
            _ILibraryBranches = ILibraryBranches;
        }

        public IActionResult Index()
        {
            var branchModels = _ILibraryBranches.GetAll();

            var listingResults = branchModels.Select(result => new BranchesIndexListingModel()
            {
                Id = result.Id,
                Name = result.Name,
                Address = result.Address,
                ImageUrl = result.ImageURL,
                OpeningClosingHours = result.OpenDate
            });

            var model = new BranchIndexModel()
            {
               Branches = listingResults
            };

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branchDetail = _ILibraryBranches.GetById(id);

            var model = new BranchDetailModel()
            {
                Id = id,
                Name = branchDetail.Name,
                Address = branchDetail.Address,
                Telephone = branchDetail.Telephone,
                ImageUrl = branchDetail.ImageURL,
                Description = branchDetail.Description,
                OpeningClosingHours = branchDetail.OpenDate
            };

            return View(model);
        }
    }
}