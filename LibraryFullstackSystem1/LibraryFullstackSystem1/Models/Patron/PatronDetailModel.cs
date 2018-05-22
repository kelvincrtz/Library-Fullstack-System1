using LibraryFullstackSystem1.Data.Model;
using System;

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
    }
}
