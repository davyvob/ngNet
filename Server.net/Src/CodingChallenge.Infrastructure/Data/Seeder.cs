using CodingChallenge.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Infrastructure.Data
{
    public class Seeder
    {




        public static void Seed(ModelBuilder modelBuilder)
        {
            CodeChallenge[] challenges = new CodeChallenge[]
            {
               new CodeChallenge{Id=Guid.NewGuid() , ChallengeNumber = 1 , Description = "Add each number inside your puzzleinput" },
               new CodeChallenge{Id=Guid.NewGuid() , ChallengeNumber = 2 , Description = "Add each number inside your puzzleinput and multiply it by the total amount of numbers inside your puzzleinput" },
               new CodeChallenge{Id=Guid.NewGuid() , ChallengeNumber = 3 , Description = "Multiply the lowest number with the highest number inside your puzzleinput" },

            };



            ApplicationUser adminApplicationUser = new ApplicationUser
            {
                Id = "00000000-0000-0000-0000-000000000001",
                UserName = "ImiAdmin",
                NormalizedUserName = "ImiAdmin".ToUpper(),
                Email = "admin@imi.be",
                NormalizedEmail = "admin@imi.be".ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA", //Random string 
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58", //Random guid string 
                CurrentSolution = 3,
                CurrentPuzzleInput = "1 2"



            };
            IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            adminApplicationUser.PasswordHash = passwordHasher.HashPassword(adminApplicationUser, "Test123?");


            ApplicationUser[] users = new ApplicationUser[]
            {
                adminApplicationUser,
            };

            IdentityRole[] roles = new IdentityRole[]
            {
                new IdentityRole
                {
                  Id = "00000000-0000-0000-0000-000000000001",
                  Name = "Admin",
                  NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole
                {
                  Id = "00000000-0000-0000-0000-000000000002",
                  Name = "User",
                  NormalizedName = "User".ToUpper()
                },
            };

            IdentityUserRole<string>[] userRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>
                {
                  RoleId = "00000000-0000-0000-0000-000000000001",
                  UserId = "00000000-0000-0000-0000-000000000001"
                },

            };

            PlayerStats[] playerStats = new PlayerStats[]
            {
                new PlayerStats("00000000-0000-0000-0000-000000000001")
            };





            modelBuilder.Entity<CodeChallenge>()
            .HasData(challenges);

            modelBuilder.Entity<ApplicationUser>()
            .HasData(users);

            modelBuilder.Entity<IdentityRole>()
            .HasData(roles);

            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(userRoles);

            modelBuilder.Entity<PlayerStats>()
           .HasData(playerStats);

        }
    }
}
