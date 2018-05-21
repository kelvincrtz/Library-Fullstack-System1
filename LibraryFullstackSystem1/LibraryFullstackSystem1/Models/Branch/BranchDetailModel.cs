using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryFullstackSystem1.Models.Branch
{
    public class BranchDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Telephone { get; set; }
        public DateTime OpeningClosingHours { get; set; }
        
        public IEnumerable<Patron> BranchPatrons { get; set; }
    }
}
