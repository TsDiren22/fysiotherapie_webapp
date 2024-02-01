using AvansFysioAppDomain.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace AvansFysioAppInfrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<PatientFile> PatientFiles { get; set; }

        public DbSet<Physiotherapist> Physiotherapists { get; set; }

        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Remark> Remarks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Physiotherapist>().HasData(
                new Physiotherapist()
                {
                    Id = 99,
                    Name = "Diren Physio",
                    Email = "abc@abc.com",
                    Phone = "0612345678",
                    IsIntern = false,
                    EmployeeId = 123,
                    BigId = 123,
                    AvailabilityStart = new DateTime(1, 1, 1, 8, 0, 0),
                    AvailabilityEnd = new DateTime(1, 1, 1, 17, 0, 0)
                },

                new Physiotherapist()
                {
                    Id = 100,
                    Name = "Direntwee Physio",
                    Email = "ghi@ghi.com",
                    Phone = "0687654321",
                    IsIntern = false,
                    EmployeeId = 321,
                    BigId = 321,
                    AvailabilityStart = new DateTime(1, 1, 1, 13, 0, 0),
                    AvailabilityEnd = new DateTime(1, 1, 1, 22, 0, 0)
                },

                new Physiotherapist()
                {
                    Id = 101,
                    Name = "Intern Physio",
                    Email = "def@def.com",
                    Phone = "0612348765",
                    IsIntern = true,
                    EmployeeId = 345,
                    BigId = null,
                    AvailabilityStart = new DateTime(1, 1, 1, 11, 0, 0),
                    AvailabilityEnd = new DateTime(1, 1, 1, 20, 0, 0)
                });

        }
    }
}
