using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatinhasMagicasAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalPhotoDataUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoDataUrl",
                table: "Animais",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoDataUrl",
                table: "Animais");
        }
    }
}
