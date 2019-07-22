namespace Vimata.Data
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class VimataDbContext : DbContext
    {
        public VimataDbContext(DbContextOptions<VimataDbContext> options) : base(options)
        {
        }
    }
}
