using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryFullstackSystem1.Data.Model
{
    public class BranchHours
    {
        public int Id { get; set; }

        public LibraryBranch Branch { get; set; }

        public int DayOfWeek { get; set; }

        public int OpenTime { get; set; }

        public int CloseTime { get; set; }
    }
}
