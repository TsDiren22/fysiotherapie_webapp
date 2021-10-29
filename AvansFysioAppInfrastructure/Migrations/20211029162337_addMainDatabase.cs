using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvansFysioAppInfrastructure.Migrations
{
    public partial class addMainDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operation",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MandatoryExplanation = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operation", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OccupationId = table.Column<int>(type: "int", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "Physiotherapists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BigId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physiotherapists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxSessions = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    DescriptionComplaintsGlobal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntakeById = table.Column<int>(type: "int", nullable: true),
                    SupervisionById = table.Column<int>(type: "int", nullable: true),
                    HeadPractitionerId = table.Column<int>(type: "int", nullable: true),
                    DateOfRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatmentPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Physiotherapists_HeadPractitionerId",
                        column: x => x.HeadPractitionerId,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Physiotherapists_IntakeById",
                        column: x => x.IntakeById,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Physiotherapists_SupervisionById",
                        column: x => x.SupervisionById,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PatientFiles_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    HeadPhysiotherapistId = table.Column<int>(type: "int", nullable: true),
                    TreatmentPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_Physiotherapists_HeadPhysiotherapistId",
                        column: x => x.HeadPhysiotherapistId,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sessions_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: true),
                    DateOfTreatment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Operation_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Operation",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Treatments_PatientFiles_PatientFileId",
                        column: x => x.PatientFileId,
                        principalTable: "PatientFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatments_Physiotherapists_PhysiotherapistId",
                        column: x => x.PhysiotherapistId,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    HeadPhysiotherapistId = table.Column<int>(type: "int", nullable: true),
                    AppointmentBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Physiotherapists_HeadPhysiotherapistId",
                        column: x => x.HeadPhysiotherapistId,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "BigId", "Email", "EmployeeId", "Name", "Phone" },
                values: new object[] { 99, 123, "abc@abc.com", 123, "Diren Physio", "0612345678" });

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "BigId", "Email", "EmployeeId", "Name", "Phone" },
                values: new object[] { 100, 321, "def@def.com", 321, "Justin Physio", "0687654321" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HeadPhysiotherapistId",
                table: "Appointments",
                column: "HeadPhysiotherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SessionId",
                table: "Appointments",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_HeadPractitionerId",
                table: "PatientFiles",
                column: "HeadPractitionerId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_IntakeById",
                table: "PatientFiles",
                column: "IntakeById");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_SupervisionById",
                table: "PatientFiles",
                column: "SupervisionById");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_HeadPhysiotherapistId",
                table: "Sessions",
                column: "HeadPhysiotherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_PatientId",
                table: "Sessions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TreatmentPlanId",
                table: "Sessions",
                column: "TreatmentPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PhysiotherapistId",
                table: "Treatments",
                column: "PhysiotherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_TypeId",
                table: "Treatments",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "PatientFiles");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physiotherapists");

            migrationBuilder.DropTable(
                name: "TreatmentPlans");
        }
    }
}
