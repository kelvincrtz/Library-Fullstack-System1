using LibraryFullstackSystem1.Data.Model;

namespace LibraryFullstackSystem1.Models.Patron
{
    public class PatronDetailModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public LibraryBranch Branch { get; set; }
        public int Fee { get; set; }
    }
}
