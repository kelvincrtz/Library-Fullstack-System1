using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryFullstackSystem1.Models.Patron
{
    public class PatronsIndexListingModel
    {
        public int Id { get; set; }
        public int LibrayCardId { get; set; }
        public string HomebranchLocation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
