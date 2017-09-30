using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TravelConnect.Web.Migrations
{
    public partial class AddCreateAndUpdateTime_Airline_Airport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Airports");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Airports",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Airports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Airports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Airlines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "Airlines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Airlines");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "Airlines");

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Airports",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
