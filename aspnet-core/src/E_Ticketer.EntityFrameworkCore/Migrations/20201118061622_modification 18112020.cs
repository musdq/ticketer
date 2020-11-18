using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Ticketer.Migrations
{
    public partial class modification18112020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTrips_AppTrains_TrainId1",
                table: "AppTrips");

            migrationBuilder.DropIndex(
                name: "IX_AppTrips_TrainId1",
                table: "AppTrips");

            migrationBuilder.DropColumn(
                name: "TrainId1",
                table: "AppTrips");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainId",
                table: "AppTrips",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AppTrips_TrainId",
                table: "AppTrips",
                column: "TrainId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTrips_AppTrains_TrainId",
                table: "AppTrips",
                column: "TrainId",
                principalTable: "AppTrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTrips_AppTrains_TrainId",
                table: "AppTrips");

            migrationBuilder.DropIndex(
                name: "IX_AppTrips_TrainId",
                table: "AppTrips");

            migrationBuilder.AlterColumn<int>(
                name: "TrainId",
                table: "AppTrips",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "TrainId1",
                table: "AppTrips",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTrips_TrainId1",
                table: "AppTrips",
                column: "TrainId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTrips_AppTrains_TrainId1",
                table: "AppTrips",
                column: "TrainId1",
                principalTable: "AppTrains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
