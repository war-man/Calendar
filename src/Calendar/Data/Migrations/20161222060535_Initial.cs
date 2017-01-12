using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Calendar.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AffectedHosts = table.Column<string>(maxLength: 50, nullable: true),
                    AffectedProjects = table.Column<string>(maxLength: 10, nullable: true),
                    Category = table.Column<string>(maxLength: 50, nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    Reference = table.Column<string>(maxLength: 50, nullable: true),
                    Result = table.Column<string>(maxLength: 100, nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(maxLength: 500, nullable: true),
                    TaskDescription = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
