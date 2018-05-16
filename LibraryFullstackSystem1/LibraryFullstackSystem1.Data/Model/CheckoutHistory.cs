﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryFullstackSystem1.Data.Model
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        
        [Required]
        public LibraryAsset LibraryAsset { get; set; }

        [Required]
        public LibraryCard LibraryCard { get; set; }

        [Required]
        public DateTime CheckedOut { get; set; }
        
        public DateTime? CheckedIn { get; set; }

    }
}
