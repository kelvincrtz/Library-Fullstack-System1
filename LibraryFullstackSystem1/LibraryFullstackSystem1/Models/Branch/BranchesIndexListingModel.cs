using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryFullstackSystem1.Models.Branch
{
    public class BranchesIndexListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime OpeningClosingHours { get; set; }
        public string Address { get; set; }
        public string OpenDate { get; set; }
    }
}
