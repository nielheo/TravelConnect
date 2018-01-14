using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TravelConnect.React.Migrations
{
    public partial class HotelDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facility",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facility", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "HotelAreaDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AreaDetail = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelAreaDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelAreaDetails_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelImageLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Caption = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Image = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Thumbnail = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelImageLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelImageLinks_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelMapLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    MapLink = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelMapLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelMapLinks_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Report = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelReports_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelRoomCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: false),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRoomCategories_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "HotelFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FacilityCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    LocationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelFacilities_Facility_LocationCode",
                        column: x => x.LocationCode,
                        principalTable: "Facility",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelRoomFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FacilityCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    LocationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomFacilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRoomFacilities_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRoomFacilities_Facility_LocationCode",
                        column: x => x.LocationCode,
                        principalTable: "Facility",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HotelRoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotelCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    RoomTypeCode = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelRoomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HotelRoomTypes_Hotels_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "Hotels",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelRoomTypes_RoomTypes_HotelCode",
                        column: x => x.HotelCode,
                        principalTable: "RoomTypes",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelAreaDetails_HotelCode",
                table: "HotelAreaDetails",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_HotelCode",
                table: "HotelFacilities",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelFacilities_LocationCode",
                table: "HotelFacilities",
                column: "LocationCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImageLinks_HotelCode",
                table: "HotelImageLinks",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelMapLinks_HotelCode",
                table: "HotelMapLinks",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelReports_HotelCode",
                table: "HotelReports",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomCategories_HotelCode",
                table: "HotelRoomCategories",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomFacilities_HotelCode",
                table: "HotelRoomFacilities",
                column: "HotelCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomFacilities_LocationCode",
                table: "HotelRoomFacilities",
                column: "LocationCode");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoomTypes_HotelCode",
                table: "HotelRoomTypes",
                column: "HotelCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelAreaDetails");

            migrationBuilder.DropTable(
                name: "HotelFacilities");

            migrationBuilder.DropTable(
                name: "HotelImageLinks");

            migrationBuilder.DropTable(
                name: "HotelMapLinks");

            migrationBuilder.DropTable(
                name: "HotelReports");

            migrationBuilder.DropTable(
                name: "HotelRoomCategories");

            migrationBuilder.DropTable(
                name: "HotelRoomFacilities");

            migrationBuilder.DropTable(
                name: "HotelRoomTypes");

            migrationBuilder.DropTable(
                name: "Facility");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
