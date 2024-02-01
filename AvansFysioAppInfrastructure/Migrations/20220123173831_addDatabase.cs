using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AvansFysioAppInfrastructure.Migrations
{
    public partial class addDatabase : Migration
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
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PictureFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    BigId = table.Column<int>(type: "int", nullable: true),
                    IsIntern = table.Column<bool>(type: "bit", nullable: false),
                    AvailabilityStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailabilityEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Physiotherapists", x => x.Id);
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
                    DescriptionComplaintsGlobal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiagnosisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiagnosisDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntakeById = table.Column<int>(type: "int", nullable: true),
                    SupervisionById = table.Column<int>(type: "int", nullable: true),
                    HeadPractitionerId = table.Column<int>(type: "int", nullable: true),
                    DateOfRegister = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemarkMadeById = table.Column<int>(type: "int", nullable: true),
                    SeenByPatient = table.Column<bool>(type: "bit", nullable: false),
                    FileByRemarkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remarks_PatientFiles_FileByRemarkId",
                        column: x => x.FileByRemarkId,
                        principalTable: "PatientFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remarks_Physiotherapists_RemarkMadeById",
                        column: x => x.RemarkMadeById,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxSessions = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    PhysiotherapistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPlans_PatientFiles_FileId",
                        column: x => x.FileId,
                        principalTable: "PatientFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreatmentPlans_Physiotherapists_PhysiotherapistId",
                        column: x => x.PhysiotherapistId,
                        principalTable: "Physiotherapists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientFileId = table.Column<int>(type: "int", nullable: false),
                    Particularities = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    TreatmentPlanId = table.Column<int>(type: "int", nullable: false),
                    HeadPhysiotherapistId = table.Column<int>(type: "int", nullable: true),
                    AppointmentMade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "AvailabilityEnd", "AvailabilityStart", "BigId", "Email", "EmployeeId", "IsIntern", "Name", "Phone" },
                values: new object[] { 99, new DateTime(1, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 123, "abc@abc.com", 123, false, "Diren Physio", "0612345678" });

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "AvailabilityEnd", "AvailabilityStart", "BigId", "Email", "EmployeeId", "IsIntern", "Name", "Phone" },
                values: new object[] { 100, new DateTime(1, 1, 1, 22, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified), 321, "ghi@ghi.com", 321, false, "Direntwee Physio", "0687654321" });

            migrationBuilder.InsertData(
                table: "Physiotherapists",
                columns: new[] { "Id", "AvailabilityEnd", "AvailabilityStart", "BigId", "Email", "EmployeeId", "IsIntern", "Name", "Phone" },
                values: new object[] { 101, new DateTime(1, 1, 1, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), null, "def@def.com", 345, true, "Intern Physio", "0612348765" });

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
                name: "IX_Remarks_FileByRemarkId",
                table: "Remarks",
                column: "FileByRemarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_RemarkMadeById",
                table: "Remarks",
                column: "RemarkMadeById");

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
                name: "IX_TreatmentPlans_FileId",
                table: "TreatmentPlans",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlans_PhysiotherapistId",
                table: "TreatmentPlans",
                column: "PhysiotherapistId");

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
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "TreatmentPlans");

            migrationBuilder.DropTable(
                name: "Operation");

            migrationBuilder.DropTable(
                name: "PatientFiles");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Physiotherapists");
        }
    }
}
