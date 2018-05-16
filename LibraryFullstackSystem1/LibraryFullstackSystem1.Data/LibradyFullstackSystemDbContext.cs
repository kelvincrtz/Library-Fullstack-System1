using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryFullstackSystem1.Data
{
    public class LibradyFullstackSystemDbContext : DbContext
    {
        public LibradyFullstackSystemDbContext(DbContextOptions options) : base(options)
        {

        }

    
    }
}
