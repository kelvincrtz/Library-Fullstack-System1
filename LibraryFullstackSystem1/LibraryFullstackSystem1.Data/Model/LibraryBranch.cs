﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryFullstackSystem1.Data.Model
{
    public class LibraryBranch
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Description { get; set; }

        public DateTime OpenDate { get; set; }

        public virtual IEnumerable<LibraryAsset> LibraryAssets { get; set; }
        
        public virtual IEnumerable<Patron> Patrons { get; set; }

        public string ImageURL { get; set; }
    }
}