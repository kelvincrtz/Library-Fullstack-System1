using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryFullstackSystem1.Data.Model
{
    public class LibraryCard
    {
        public int Id { get; set; }
        
        public decimal Fees { get; set; }

        public DateTime Created { get; set; }

        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
