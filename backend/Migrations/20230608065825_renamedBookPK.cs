using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bookstore.Migrations
{
    /// <inheritdoc />
    public partial class renamedBookPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_Books_BooksId",
                table: "BookUser");

            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_Users_UsersId",
                table: "BookUser");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "BookUser",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "BookUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_Books_BookId",
                table: "BookUser",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_Books_UserId",
                table: "BookUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUser_Books_BooksBookId",
                table: "BookUser");

            migrationBuilder.RenameColumn(
                name: "BooksBookId",
                table: "BookUser",
                newName: "BooksId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Books",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUser_Books_BooksId",
                table: "BookUser",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
