using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ForeignKeyDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ManagerID",
                table: "Staffs",
                newName: "AdminID");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Travels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Travels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Information",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travels_AdminID",
                table: "Travels",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_StaffID",
                table: "Travels",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_StatusID",
                table: "Travels",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_StaffID",
                table: "Admins",
                column: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Staffs_StaffID",
                table: "Admins",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Admins_AdminID",
                table: "Staffs",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Admins_AdminID",
                table: "Travels",
                column: "AdminID",
                principalTable: "Admins",
                principalColumn: "AdminID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Staffs_StaffID",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Admins_AdminID",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Admins_AdminID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_AdminID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_StaffID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_StatusID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Admins_StaffID",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Travels");

            migrationBuilder.RenameColumn(
                name: "AdminID",
                table: "Staffs",
                newName: "ManagerID");

            migrationBuilder.AlterColumn<string>(
                name: "Information",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
