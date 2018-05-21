using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryFullstackSystem1.Models.Branch
{
    public class BranchIndexModel
    {
        public IEnumerable<BranchesIndexListingModel> Branches { get; set; }
    }
}
