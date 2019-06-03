using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GUIEksamen2019SmartCityWeb.Models;

namespace GUIEksamen2019SmartCityWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GUIEksamen2019SmartCityWeb.Models.LocationModel> LocationModel { get; set; }
        public DbSet<GUIEksamen2019SmartCityWeb.Models.TreeSensorModel> TreeSensorModel { get; set; }
    }
}
