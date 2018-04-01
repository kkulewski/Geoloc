using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Geoloc.Migrations
{
    public partial class ChangeMeetingHost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Meetings_MeetingId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_HostId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_HostId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MeetingId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MeetingId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "HostId",
                table: "Meetings",
                newName: "MeetingHostId");

            migrationBuilder.CreateTable(
                name: "AppUserInMeetings",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(nullable: false),
                    MeetingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserInMeetings", x => new { x.AppUserId, x.MeetingId });
                    table.ForeignKey(
                        name: "FK_AppUserInMeetings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserInMeetings_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserInMeetings_MeetingId",
                table: "AppUserInMeetings",
                column: "MeetingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserInMeetings");

            migrationBuilder.RenameColumn(
                name: "MeetingHostId",
                table: "Meetings",
                newName: "HostId");

            migrationBuilder.AddColumn<Guid>(
                name: "MeetingId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_HostId",
                table: "Meetings",
                column: "HostId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_HostId",
                table: "Meetings",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
