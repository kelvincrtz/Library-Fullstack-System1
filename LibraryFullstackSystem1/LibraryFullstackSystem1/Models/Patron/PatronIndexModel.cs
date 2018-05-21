using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryFullstackSystem1.Models.Patron
{
    public class PatronIndexModel
    {
        public IEnumerable<PatronsIndexListingModel> Patrons { get; set; }
    }
}
