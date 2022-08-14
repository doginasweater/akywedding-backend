using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace akywedding_backend.Migrations
{
    public partial class redodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meals");

            migrationBuilder.DropTable(
                name: "receptions");

            migrationBuilder.RenameColumn(
                name: "isAttending",
                table: "guests",
                newName: "is_child");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "mealOptions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "mealOptions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Partyid",
                table: "guests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "guests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "dietary_restrictions",
                table: "guests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_attending",
                table: "guests",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "meal_choiceid",
                table: "guests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "guests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "parties",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parties", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rsvps",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    music = table.Column<string>(type: "text", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: false),
                    partyid = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rsvps", x => x.id);
                    table.ForeignKey(
                        name: "FK_rsvps_parties_partyid",
                        column: x => x.partyid,
                        principalTable: "parties",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guests_meal_choiceid",
                table: "guests",
                column: "meal_choiceid");

            migrationBuilder.CreateIndex(
                name: "IX_guests_Partyid",
                table: "guests",
                column: "Partyid");

            migrationBuilder.CreateIndex(
                name: "IX_rsvps_partyid",
                table: "rsvps",
                column: "partyid");

            migrationBuilder.AddForeignKey(
                name: "FK_guests_mealOptions_meal_choiceid",
                table: "guests",
                column: "meal_choiceid",
                principalTable: "mealOptions",
                principalColumn: "id");

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
                name: "FK_guests_mealOptions_meal_choiceid",
                table: "guests");

            migrationBuilder.DropForeignKey(
                name: "FK_guests_parties_Partyid",
                table: "guests");

            migrationBuilder.DropTable(
                name: "rsvps");

            migrationBuilder.DropTable(
                name: "parties");

            migrationBuilder.DropIndex(
                name: "IX_guests_meal_choiceid",
                table: "guests");

            migrationBuilder.DropIndex(
                name: "IX_guests_Partyid",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "mealOptions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "mealOptions");

            migrationBuilder.DropColumn(
                name: "Partyid",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "dietary_restrictions",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "is_attending",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "meal_choiceid",
                table: "guests");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "guests");

            migrationBuilder.RenameColumn(
                name: "is_child",
                table: "guests",
                newName: "isAttending");

            migrationBuilder.CreateTable(
                name: "meals",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    choiceid = table.Column<long>(type: "bigint", nullable: false),
                    dietaryRestrictions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meals", x => x.id);
                    table.ForeignKey(
                        name: "FK_meals_mealOptions_choiceid",
                        column: x => x.choiceid,
                        principalTable: "mealOptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "receptions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    guestid = table.Column<long>(type: "bigint", nullable: false),
                    comments = table.Column<string>(type: "text", nullable: false),
                    music = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_receptions_guests_guestid",
                        column: x => x.guestid,
                        principalTable: "guests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meals_choiceid",
                table: "meals",
                column: "choiceid");

            migrationBuilder.CreateIndex(
                name: "IX_receptions_guestid",
                table: "receptions",
                column: "guestid");
        }
    }
}
