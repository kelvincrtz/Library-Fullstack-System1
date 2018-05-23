using LibraryFullstackSystem1.Data.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraryFullstackSystem1.Models.Patron
{
    public class PatronDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Branch { get; set; }
        public decimal Fee { get; set; }

        public IEnumerable<PatronHoldDetailModel> PatronHolds { get; set; }
        public IEnumerable<PatronCheckoutDetailModel> PatronCheckouts { get; set; }

    }

    public class PatronHoldDetailModel
    {
        public int AssetId { get; set; }
        public string AssetTitle { get; set; }
        public DateTime HoldPlaced { get; set; }
    }

    public class PatronCheckoutDetailModel
    {
        public int LibraryAssetId { get; set; }
        public int LibraryCardId { get; set; }
        public string AssetTitle { get; set; }
        public DateTime Since { get; set; }
        public DateTime Until { get; set; }
    }
}
