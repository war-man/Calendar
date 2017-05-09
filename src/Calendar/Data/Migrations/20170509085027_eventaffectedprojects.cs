using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calendar.Data.Migrations
{
    public partial class eventaffectedprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 200,
                nullable: false);

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

            migrationBuilder.AlterColumn<string>(
                name: "AffectedProjects",
                table: "Event",
                maxLength: 150,
                nullable: false);
        }
    }
}
