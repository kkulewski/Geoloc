using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Geoloc.Migrations
{
    public partial class RenameUserRelationToRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_InvitedUserId",
                table: "UserRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRelations_AspNetUsers_InvitingUserId",
                table: "UserRelations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRelations",
                table: "UserRelations");

            migrationBuilder.RenameTable(
                name: "UserRelations",
                newName: "Relations");

            migrationBuilder.RenameIndex(
                name: "IX_UserRelations_InvitingUserId",
                table: "Relations",
                newName: "IX_Relations_InvitingUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRelations_InvitedUserId",
                table: "Relations",
                newName: "IX_Relations_InvitedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relations",
                table: "Relations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_AspNetUsers_InvitedUserId",
                table: "Relations",
                column: "InvitedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Relations_AspNetUsers_InvitingUserId",
                table: "Relations",
                column: "InvitingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relations_AspNetUsers_InvitedUserId",
                table: "Relations");

            migrationBuilder.DropForeignKey(
                name: "FK_Relations_AspNetUsers_InvitingUserId",
                table: "Relations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relations",
                table: "Relations");

            migrationBuilder.RenameTable(
                name: "Relations",
                newName: "UserRelations");

            migrationBuilder.RenameIndex(
                name: "IX_Relations_InvitingUserId",
                table: "UserRelations",
                newName: "IX_UserRelations_InvitingUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Relations_InvitedUserId",
                table: "UserRelations",
                newName: "IX_UserRelations_InvitedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRelations",
                table: "UserRelations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AppUserId",
                table: "Locations",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_InvitedUserId",
                table: "UserRelations",
                column: "InvitedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRelations_AspNetUsers_InvitingUserId",
                table: "UserRelations",
                column: "InvitingUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
