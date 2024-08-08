using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_Removed_Admin_Stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Admins_AdminID",
                table: "Staffs");

            migrationBuilder.DropForeignKey(
                name: "FK_Travels_Admins_AdminID",
                table: "Travels");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Travels_AdminID",
                table: "Travels");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Travels");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Staffs");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Staffs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Staffs");

            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Travels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffID = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_Admins_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Travels_AdminID",
                table: "Travels",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_AdminID",
                table: "Staffs",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_StaffID",
                table: "Admins",
                column: "StaffID");

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
        }
    }
}
