﻿using LibraryFullstackSystem1.Data;
using LibraryFullstackSystem1.Models.Branch;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryFullstackSystem1.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly ILibraryBranches _ILibraryBranches;

        public LibraryBranchController(ILibraryBranches ILibraryBranches)
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
    }
}