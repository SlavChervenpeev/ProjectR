using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectR.Models;

namespace System_for_notebook_management.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notebook> Notebooks { get; set; }

        public DbSet<NotebookSpecifications> NotebookSpecifications { get; set; }

        public DbSet<Company> Companies { get; set; }
    }
}
