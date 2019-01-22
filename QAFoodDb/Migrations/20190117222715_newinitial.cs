using Microsoft.EntityFrameworkCore.Migrations;

namespace QAFoodDb.Migrations
{
    public partial class newinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewCategory",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewScore",
                table: "Review");

            migrationBuilder.CreateIndex(
                name: "IX_Review_FoodId",
                table: "Review",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Food_FoodId",
                table: "Review",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Food_FoodId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_FoodId",
                table: "Review");

            migrationBuilder.AddColumn<int>(
                name: "ReviewCategory",
                table: "Review",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewScore",
                table: "Review",
                nullable: false,
                defaultValue: 0);
        }
    }
}
