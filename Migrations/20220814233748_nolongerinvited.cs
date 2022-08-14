using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace akywedding_backend.Migrations
{
    public partial class nolongerinvited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_parties_partyid",
                table: "guests");

            migrationBuilder.RenameColumn(
                name: "partyid",
                table: "guests",
                newName: "Partyid");

            migrationBuilder.RenameIndex(
                name: "IX_guests_partyid",
                table: "guests",
                newName: "IX_guests_Partyid");

            migrationBuilder.AlterColumn<long>(
                name: "Partyid",
                table: "guests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_parties_Partyid",
                table: "guests",
                column: "Partyid",
                principalTable: "parties",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_guests_parties_Partyid",
                table: "guests");

            migrationBuilder.RenameColumn(
                name: "Partyid",
                table: "guests",
                newName: "partyid");

            migrationBuilder.RenameIndex(
                name: "IX_guests_Partyid",
                table: "guests",
                newName: "IX_guests_partyid");

            migrationBuilder.AlterColumn<long>(
                name: "partyid",
                table: "guests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_guests_parties_partyid",
                table: "guests",
                column: "partyid",
                principalTable: "parties",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
