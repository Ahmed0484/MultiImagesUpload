using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiImagesUpload.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "UploadedImages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "UploadedImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "UploadedImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "UploadedImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
