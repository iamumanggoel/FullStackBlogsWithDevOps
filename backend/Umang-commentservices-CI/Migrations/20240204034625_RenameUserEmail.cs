using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentServices.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Comments",
                newName: "UserEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Comments",
                newName: "userEmail");

            migrationBuilder.AddColumn<bool>(
                name: "Like",
                table: "Comments",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
