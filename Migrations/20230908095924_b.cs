using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationStatus_ApplicationStatusId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationType_ApplicationTypeId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_InventoryType_InventoryTypeId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donation",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "DONATION_ID",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "DONATION_TYPE_ID",
                table: "Donation");

            migrationBuilder.RenameColumn(
                name: "SPONSOR_ID",
                table: "Donation",
                newName: "DonationAmount");

            migrationBuilder.RenameColumn(
                name: "DONATION_NAME",
                table: "Donation",
                newName: "DonationName");

            migrationBuilder.RenameColumn(
                name: "DONATION_DESCRIPTION",
                table: "Donation",
                newName: "DonationDescription");

            migrationBuilder.RenameColumn(
                name: "DATE_RECIEVED",
                table: "Donation",
                newName: "DateReceived");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentTypeId",
                table: "Student",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DonationId",
                table: "Donation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DonationTypeId",
                table: "Donation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SponsorId",
                table: "Donation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationTypeId",
                table: "Application",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationStatusId",
                table: "Application",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantGender",
                table: "Application",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donation",
                table: "Donation",
                column: "DonationId");

            migrationBuilder.CreateTable(
                name: "AttendanceType",
                columns: table => new
                {
                    AttendanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceTypeDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceType", x => x.AttendanceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiary",
                columns: table => new
                {
                    BeneficiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeneficiarySurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeneficiaryIdNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeneficiaryContactNumber = table.Column<int>(type: "int", nullable: false),
                    BeneficiaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiary", x => x.BeneficiaryId);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BookGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => x.BookGenreId);
                });

            migrationBuilder.CreateTable(
                name: "BookStatus",
                columns: table => new
                {
                    BookStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStatus", x => x.BookStatusId);
                });

            migrationBuilder.CreateTable(
                name: "CafeteriaType",
                columns: table => new
                {
                    CafeteriaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CafeteriaTypeDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeteriaType", x => x.CafeteriaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    CheckInId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.CheckInId);
                });

            migrationBuilder.CreateTable(
                name: "CheckOut",
                columns: table => new
                {
                    CheckOutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeneficiaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOut", x => x.CheckOutId);
                });

            migrationBuilder.CreateTable(
                name: "DonationType",
                columns: table => new
                {
                    DonationTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonationTypeDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationType", x => x.DonationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "SponsorType",
                columns: table => new
                {
                    SponsorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorTypeDescription = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorType", x => x.SponsorTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Cafeteria",
                columns: table => new
                {
                    CafeteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CafeteriaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CafeteriaDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cafeteria", x => x.CafeteriaId);
                    table.ForeignKey(
                        name: "FK_Cafeteria_CafeteriaType_CafeteriaTypeId",
                        column: x => x.CafeteriaTypeId,
                        principalTable: "CafeteriaType",
                        principalColumn: "CafeteriaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    SponsorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SponsorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SponsorEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SponsorContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.SponsorId);
                    table.ForeignKey(
                        name: "FK_Sponsor_SponsorType_SponsorTypeId",
                        column: x => x.SponsorTypeId,
                        principalTable: "SponsorType",
                        principalColumn: "SponsorTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CafeteriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_Attendance_AttendanceType_AttendanceTypeId",
                        column: x => x.AttendanceTypeId,
                        principalTable: "AttendanceType",
                        principalColumn: "AttendanceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Cafeteria_CafeteriaId",
                        column: x => x.CafeteriaId,
                        principalTable: "Cafeteria",
                        principalColumn: "CafeteriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_AttendanceTypeId",
                table: "Attendance",
                column: "AttendanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CafeteriaId",
                table: "Attendance",
                column: "CafeteriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceType_AttendanceTypeDescription",
                table: "AttendanceType",
                column: "AttendanceTypeDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiary_BeneficiaryIdNumber",
                table: "Beneficiary",
                column: "BeneficiaryIdNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_Isbn",
                table: "Book",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenreDescription",
                table: "BookGenre",
                column: "GenreDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookStatus_BookDescription",
                table: "BookStatus",
                column: "BookDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cafeteria_CafeteriaTypeId",
                table: "Cafeteria",
                column: "CafeteriaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CafeteriaType_CafeteriaTypeDescription",
                table: "CafeteriaType",
                column: "CafeteriaTypeDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_CityName",
                table: "City",
                column: "CityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DonationType_DonationTypeDescription",
                table: "DonationType",
                column: "DonationTypeDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Province_ProvinceName",
                table: "Province",
                column: "ProvinceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sponsor_SponsorTypeId",
                table: "Sponsor",
                column: "SponsorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SponsorType_SponsorTypeDescription",
                table: "SponsorType",
                column: "SponsorTypeDescription",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationStatus_ApplicationStatusId",
                table: "Application",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "ApplicationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationType_ApplicationTypeId",
                table: "Application",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_InventoryType_InventoryTypeId",
                table: "Inventory",
                column: "InventoryTypeId",
                principalTable: "InventoryType",
                principalColumn: "InventoryTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student",
                column: "StudentTypeId",
                principalTable: "StudentType",
                principalColumn: "StudentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationStatus_ApplicationStatusId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Application_ApplicationType_ApplicationTypeId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_InventoryType_InventoryTypeId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Beneficiary");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.DropTable(
                name: "BookStatus");

            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropTable(
                name: "CheckOut");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "DonationType");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "AttendanceType");

            migrationBuilder.DropTable(
                name: "Cafeteria");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "SponsorType");

            migrationBuilder.DropTable(
                name: "CafeteriaType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Donation",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "DonationId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "DonationTypeId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "ApplicantGender",
                table: "Application");

            migrationBuilder.RenameColumn(
                name: "DonationName",
                table: "Donation",
                newName: "DONATION_NAME");

            migrationBuilder.RenameColumn(
                name: "DonationDescription",
                table: "Donation",
                newName: "DONATION_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "DonationAmount",
                table: "Donation",
                newName: "SPONSOR_ID");

            migrationBuilder.RenameColumn(
                name: "DateReceived",
                table: "Donation",
                newName: "DATE_RECIEVED");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentTypeId",
                table: "Student",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DONATION_ID",
                table: "Donation",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "DONATION_TYPE_ID",
                table: "Donation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationTypeId",
                table: "Application",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationStatusId",
                table: "Application",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Donation",
                table: "Donation",
                column: "DONATION_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationStatus_ApplicationStatusId",
                table: "Application",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "ApplicationStatusId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Application_ApplicationType_ApplicationTypeId",
                table: "Application",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "ApplicationTypeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_EmployeeType_EmployeeTypeId",
                table: "Employee",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_InventoryType_InventoryTypeId",
                table: "Inventory",
                column: "InventoryTypeId",
                principalTable: "InventoryType",
                principalColumn: "InventoryTypeId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_StudentType_StudentTypeId",
                table: "Student",
                column: "StudentTypeId",
                principalTable: "StudentType",
                principalColumn: "StudentTypeId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
