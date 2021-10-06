using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using Microsoft.EntityFrameworkCore;

namespace AvansFysioAppInfrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
        
        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientFile> PatientFiles { get; set; }

        public DbSet<Physiotherapist> Physiotherapists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Physiotherapist>().HasData(
                new Physiotherapist()
                {
                    Id = 99,
                    Name = "Diren Physio",
                    Email = "abc@def.com",
                    Phone = "0612345678",
                    EmployeeId = 123,
                    BigId = 123
                },
                
                new Physiotherapist()
                {
                    Id = 100,
                    Name = "Justin Physio",
                    Email = "ghi@jkl.com",
                    Phone = "0687654321",
                    EmployeeId = 321,
                    BigId = 321
                });
        }
    }
}
