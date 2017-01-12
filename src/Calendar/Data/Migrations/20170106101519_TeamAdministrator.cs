using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Calendar.Data.Migrations
{
    public partial class TeamAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Administrator = table.Column<string>(maxLength: 5, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 5, nullable: false),
                    Owner = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Team",
                maxLength: 5,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Team",
                maxLength: 50,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CalendarStyle",
                table: "Team",
                maxLength: 15,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Team",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Team",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CalendarStyle",
                table: "Team",
                nullable: true);
        }
    }
}
