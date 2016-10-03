using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NETEventManager.Migrations
{
    public partial class AddLocationToVenues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_AspNetUsers_UserId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Venues_UserId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Venues");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(nullable: true),
                    cc = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    crossStreet = table.Column<string>(nullable: true),
                    distance = table.Column<int>(nullable: false),
                    lat = table.Column<double>(nullable: false),
                    lng = table.Column<double>(nullable: false),
                    postalCode = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Venues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "Venues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_LocationId",
                table: "Venues",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_Location_LocationId",
                table: "Venues",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Venues_Location_LocationId",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Venues_LocationId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "Venues");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Venues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_UserId",
                table: "Venues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Venues_AspNetUsers_UserId",
                table: "Venues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
