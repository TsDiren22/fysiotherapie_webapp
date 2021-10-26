    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFysioAppDomain.Domain;
using Microsoft.EntityFrameworkCore;


namespace AvansFysioAppInfrastructure.Data
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
        {
        }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Diagnosis> Diagnoses { get; set; }

        public List<Diagnosis> GetAllDiagnoses()
        {
            List<Diagnosis> list = new List<Diagnosis>();

            StreamReader readerDiagnosis =
                new StreamReader(
                    "..\\AvansFysioAppInfrastructure\\Seed\\Vektis lijst diagnoses gecorrigeerd (2).csv");
            {
                bool isFirst = true;
                while (!readerDiagnosis.EndOfStream)
                {
                    var line = readerDiagnosis.ReadLine();
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                    string[] values = line.Split(',');
                    string value = values[0];
                    string pathology = "";
                    if (values.Length > 3)
                    {
                        for (int i = 2; i <= values.Length - 1; i++)
                        {
                            pathology += values[i];
                            if (i != values.Length - 1) pathology += ",";
                        }
                    }
                    else
                    {
                        pathology = values[2];
                    }

                    pathology = pathology.Replace("\"", "");

                    Diagnosis temp = new Diagnosis()
                    {
                        Code = value,
                        LocationOnBody = values[1],
                        Pathology = pathology
                    };

                    list.Add(temp);
                }
            }

            return list;
        }

        public List<Operation> GetAllOperations()
        {
            List<Operation> list = new List<Operation>();

            StreamReader readerOperation = new StreamReader("..\\AvansFysioAppInfrastructure\\Seed\\Vektis lijst verrichtingen (1).csv");
            {

                bool isFirst = true;
                while (!readerOperation.EndOfStream)
                {
                    var line = readerOperation.ReadLine();
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                    string[] values = line.Split(',');
                    bool explanation = values[values.Length - 1].Equals("Ja");

                    string description = "";

                    if (values.Length > 3)
                    {
                        for (int i = 1; i <= values.Length - 2; i++)
                        {
                            description += values[i];
                            if (i != values.Length - 2) description += ",";
                        }
                    }
                    else
                    {
                        description = values[1];
                    }

                    description = description.Replace("\"", "");

                    Operation temp = new Operation()
                    {
                        Value = values[0],
                        Description = description,
                        MandatoryExplanation = explanation
                    };
                    list.Add(temp);
                }
            }
            return list;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Diagnosis>().HasData(GetAllDiagnoses());
            modelBuilder.Entity<Operation>().HasData(GetAllOperations());
        }
    }
}
