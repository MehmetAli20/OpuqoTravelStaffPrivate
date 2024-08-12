using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_AdminID_Added_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels");

            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Travels",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StaffID1",
                table: "Travels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Staffs",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Travels_AdminID",
                table: "Travels",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_StaffID1",
                table: "Travels",
                column: "StaffID1");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs",
                column: "AdminID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_AdminID",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Staffs_StaffID1",
                table: "Travels");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_AdminID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Travels_StaffID1",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "StaffID1",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Staffs");

            migrationBuilder.AddForeignKey(
                name: "FK_Travels_Statuses_StatusID",
                table: "Travels",
                column: "StatusID",
                principalTable: "Statuses",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
