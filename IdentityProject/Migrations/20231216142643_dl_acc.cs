using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Migrations
{
    public partial class dl_acc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
        table: "AspNetUsers",
        keyColumn: "Email",
        keyValue: "thanhtran.8676@gmail.com");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
