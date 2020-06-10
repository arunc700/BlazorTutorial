using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearningWebApi.Migrations
{
    public partial class BlazorTestingDBCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "HR" },
                    { 3, "Admin" },
                    { 4, "Payroll" }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "EmployeeId", "DateOfBirth", "DepartmentId", "Email", "FirstName", "Gender", "LastName", "PhotoPath" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "arun@gmail.com", "Arun", 0, "Singh", "images/img1.png" },
                    { 2, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "ram@gmail.com", "Ram", 0, "Pandey", "images/img2.png" },
                    { 3, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "pankaj@gmail.com", "Pankaj", 0, "Pandey", "images/img3.png" },
                    { 4, new DateTime(1990, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "nisha@gmail.com", "Nisha", 1, "Singh", "images/img3.png" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
