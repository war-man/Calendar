using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Calendar.Data.Migrations
{
    public partial class Attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedByDisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EventID = table.Column<int>(nullable: false),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(maxLength: 500, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedByDisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acknowledgement_EventID",
                table: "Acknowledgement",
                column: "EventID");

            migrationBuilder.AddForeignKey(
                name: "FK_Acknowledgement_Event_EventID",
                table: "Acknowledgement",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acknowledgement_Event_EventID",
                table: "Acknowledgement");

            migrationBuilder.DropIndex(
                name: "IX_Acknowledgement_EventID",
                table: "Acknowledgement");

            migrationBuilder.DropTable(
                name: "Attachment");
        }
    }
}
