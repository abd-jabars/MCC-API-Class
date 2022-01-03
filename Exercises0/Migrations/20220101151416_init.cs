using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Exercises0.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Employees",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employees", x => x.NIK);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Universities", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Accounts",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<int>(type: "int", nullable: false),
                    ExpiredToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Accounts", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Accounts_TB_M_Employees_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employees",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_TB_M_Educations_TB_M_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "TB_M_Universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_AccountRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNIK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_AccountRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRoles_TB_M_Accounts_AccountNIK",
                        column: x => x.AccountNIK,
                        principalTable: "TB_M_Accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRoles_TB_M_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TB_M_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_Profilings",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_Profilings", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_TR_Profilings_TB_M_Accounts_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Accounts",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_TR_Profilings_TB_M_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "TB_M_Educations",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Educations_UniversityId",
                table: "TB_M_Educations",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRoles_AccountNIK",
                table: "TB_TR_AccountRoles",
                column: "AccountNIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRoles_RoleId",
                table: "TB_TR_AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_Profilings_EducationId",
                table: "TB_TR_Profilings",
                column: "EducationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TR_AccountRoles");

            migrationBuilder.DropTable(
                name: "TB_TR_Profilings");

            migrationBuilder.DropTable(
                name: "TB_M_Roles");

            migrationBuilder.DropTable(
                name: "TB_M_Accounts");

            migrationBuilder.DropTable(
                name: "TB_M_Educations");

            migrationBuilder.DropTable(
                name: "TB_M_Employees");

            migrationBuilder.DropTable(
                name: "TB_M_Universities");
        }
    }
}
