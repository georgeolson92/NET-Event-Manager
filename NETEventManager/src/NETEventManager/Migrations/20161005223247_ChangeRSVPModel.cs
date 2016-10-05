using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NETEventManager.Migrations
{
    public partial class ChangeRSVPModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttendeeId",
                table: "RSVPs");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "RSVPs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "RSVPs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RSVPs_EventId",
                table: "RSVPs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_RSVPs_UserId",
                table: "RSVPs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_Events_EventId",
                table: "RSVPs",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RSVPs_AspNetUsers_UserId",
                table: "RSVPs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_Events_EventId",
                table: "RSVPs");

            migrationBuilder.DropForeignKey(
                name: "FK_RSVPs_AspNetUsers_UserId",
                table: "RSVPs");

            migrationBuilder.DropIndex(
                name: "IX_RSVPs_EventId",
                table: "RSVPs");

            migrationBuilder.DropIndex(
                name: "IX_RSVPs_UserId",
                table: "RSVPs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RSVPs");

            migrationBuilder.AddColumn<int>(
                name: "AttendeeId",
                table: "RSVPs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "RSVPs",
                nullable: false);
        }
    }
}
