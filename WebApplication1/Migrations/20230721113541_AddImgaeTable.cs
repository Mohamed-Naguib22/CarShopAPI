using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImgaeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img_url",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img_url",
                table: "Cars");
        }
    }
}
