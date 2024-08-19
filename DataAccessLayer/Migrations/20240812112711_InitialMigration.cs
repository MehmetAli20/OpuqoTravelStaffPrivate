using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_AdminID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_StaffID1",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_StaffID1",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "StaffID1",
                table: "Travels");

            migrationBuilder.AlterColumn<string>(
                name: "Vehicle",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Stay",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Staffs_AdminID",
                table: "Staffs",
                column: "AdminID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_AdminID",
                table: "Travels",
                column: "AdminID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_AdminID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels");

            migrationBuilder.AlterColumn<string>(
                name: "Vehicle",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Stay",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Travels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "StaffID1",
                table: "Travels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travels_StaffID1",
                table: "Travels",
                column: "StaffID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Staffs_AdminID",
                table: "Staffs",
                column: "AdminID",
                principalTable: "Staffs",
                principalColumn: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_AdminID",
                table: "Travels",
                column: "AdminID",
                principalTable: "Staffs",
                principalColumn: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_StaffID",
                table: "Travels",
                column: "StaffID",
                principalTable: "Staffs",
                principalColumn: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Staffs_StaffID1",
                table: "Travels",
                column: "StaffID1",
                principalTable: "Staffs",
                principalColumn: "StaffID");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID");
        }
    }
}
