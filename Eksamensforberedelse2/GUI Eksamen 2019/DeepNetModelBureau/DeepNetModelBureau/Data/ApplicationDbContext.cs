using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DeepNetModelBureau.Models;

namespace DeepNetModelBureau.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DeepNetModelBureau.Models.Board> Board { get; set; }
        public DbSet<DeepNetModelBureau.Models.Bill> Bill { get; set; }
    }
}
