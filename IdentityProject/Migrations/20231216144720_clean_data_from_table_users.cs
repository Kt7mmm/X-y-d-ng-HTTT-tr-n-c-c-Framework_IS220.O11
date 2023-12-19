using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Migrations
{
    public partial class clean_data_from_table_users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE cus_phone IS NOT NULL;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
