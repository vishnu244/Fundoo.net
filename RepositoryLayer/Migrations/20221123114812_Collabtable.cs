using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Collabtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesEntity_UserTable_UserId",
                table: "NotesEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesEntity",
                table: "NotesEntity");

            migrationBuilder.RenameTable(
                name: "NotesEntity",
                newName: "NotesTable");

            migrationBuilder.RenameIndex(
                name: "IX_NotesEntity_UserId",
                table: "NotesTable",
                newName: "IX_NotesTable_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesTable",
                table: "NotesTable",
                column: "NoteID");

            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollabEmail = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabID);
                    table.ForeignKey(
                        name: "FK_CollabTable_NotesTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NotesTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteID",
                table: "CollabTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesTable_UserTable_UserId",
                table: "NotesTable");

            migrationBuilder.DropTable(
                name: "CollabTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesTable",
                table: "NotesTable");

            migrationBuilder.RenameTable(
                name: "NotesTable",
                newName: "NotesEntity");

            migrationBuilder.RenameIndex(
                name: "IX_NotesTable_UserId",
                table: "NotesEntity",
                newName: "IX_NotesEntity_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesEntity",
                table: "NotesEntity",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesEntity_UserTable_UserId",
                table: "NotesEntity",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
