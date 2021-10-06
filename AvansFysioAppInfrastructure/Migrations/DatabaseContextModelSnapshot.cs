﻿// <auto-generated />
using System;
using AvansFysioAppInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AvansFysioAppInfrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AvansFysioAppDomain.Domain.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OccupationId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("AvansFysioAppDomain.Domain.PatientFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionComplaintsGlobal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosisNumber")
                        .HasColumnType("int");

                    b.Property<int?>("HeadPractitionerId")
                        .HasColumnType("int");

                    b.Property<int?>("IntakeById")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupervisionById")
                        .HasColumnType("int");

                    b.Property<string>("TreatmentPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Treatments")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadPractitionerId");

                    b.HasIndex("IntakeById");

                    b.HasIndex("PatientId");

                    b.HasIndex("SupervisionById");

                    b.ToTable("PatientFiles");
                });

            modelBuilder.Entity("AvansFysioAppDomain.Domain.Physiotherapist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BigId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Physiotherapists");

                    b.HasData(
                        new
                        {
                            Id = 99,
                            BigId = 123,
                            Email = "abc@def.com",
                            EmployeeId = 123,
                            Name = "Diren Physio",
                            Phone = "0612345678"
                        },
                        new
                        {
                            Id = 100,
                            BigId = 321,
                            Email = "ghi@jkl.com",
                            EmployeeId = 321,
                            Name = "Justin Physio",
                            Phone = "0687654321"
                        });
                });

            modelBuilder.Entity("AvansFysioAppDomain.Domain.PatientFile", b =>
                {
                    b.HasOne("AvansFysioAppDomain.Domain.Physiotherapist", "HeadPractitioner")
                        .WithMany()
                        .HasForeignKey("HeadPractitionerId");

                    b.HasOne("AvansFysioAppDomain.Domain.Physiotherapist", "IntakeBy")
                        .WithMany()
                        .HasForeignKey("IntakeById");

                    b.HasOne("AvansFysioAppDomain.Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("AvansFysioAppDomain.Domain.Physiotherapist", "SupervisionBy")
                        .WithMany()
                        .HasForeignKey("SupervisionById");

                    b.Navigation("HeadPractitioner");

                    b.Navigation("IntakeBy");

                    b.Navigation("Patient");

                    b.Navigation("SupervisionBy");
                });
#pragma warning restore 612, 618
        }
    }
}
