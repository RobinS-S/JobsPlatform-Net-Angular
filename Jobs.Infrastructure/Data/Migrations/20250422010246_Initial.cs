using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Jobs.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Sqlite:InitSpatialMetaData", true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Location_AddressLines = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Location_City = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Location_StateProvince = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Location_PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Location_CountryCode = table.Column<string>(type: "TEXT", unicode: false, maxLength: 2, nullable: false),
                    Location_GeoLocation = table.Column<Point>(type: "POINT", nullable: true)
                        .Annotation("Sqlite:Srid", 4326),
                    Website = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4096, nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: true),
                    MinHoursPerWeek = table.Column<int>(type: "INTEGER", nullable: true),
                    MaxHoursPerWeek = table.Column<int>(type: "INTEGER", nullable: true),
                    AllowsRemoteWork = table.Column<bool>(type: "INTEGER", nullable: true),
                    ContactEmail = table.Column<string>(type: "TEXT", maxLength: 320, nullable: true),
                    JobUrl = table.Column<string>(type: "TEXT", maxLength: 2048, nullable: true),
                    CompanyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_CompanyId",
                table: "JobPostings",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
