using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppWeb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Identitypdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonNam",
                table: "AspNetUsers",
                newName: "PersonName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "AspNetUsers",
                newName: "PersonNam");
        }
    }
}
