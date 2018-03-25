using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Geoloc.Migrations
{
    public partial class AddOwnerToMeeting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Meetings_MeetingId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MeetingId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Meetings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_AppUserId",
                table: "Meetings",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_AppUserId",
                table: "Meetings",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_AppUserId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_AppUserId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Meetings");

            migrationBuilder.AddColumn<Guid>(
                name: "MeetingId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MeetingId",
                table: "AspNetUsers",
                column: "MeetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Meetings_MeetingId",
                table: "AspNetUsers",
                column: "MeetingId",
                principalTable: "Meetings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
