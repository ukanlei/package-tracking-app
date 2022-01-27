using Microsoft.EntityFrameworkCore.Migrations;

namespace package_tracking_app.Migrations
{
    public partial class PackageToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Packages",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ApplicationUserId1",
                table: "Packages",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_AspNetUsers_ApplicationUserId1",
                table: "Packages",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_AspNetUsers_ApplicationUserId1",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Packages_ApplicationUserId1",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Packages");
        }
    }
}
