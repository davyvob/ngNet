using CodingChallenge.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infrastructure.Data
{
    public class CodingChallengeDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<CodeChallenge> Challenges { get; set; }
        public DbSet<CompletedCodeChallenge> CompletedChallenges { get; set; }

        public DbSet<PlayerStats> PlayerStats { get; set; }

        public CodingChallengeDbContext(DbContextOptions<CodingChallengeDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            Seeder.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
