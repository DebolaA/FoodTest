using Microsoft.EntityFrameworkCore.Migrations;

namespace QAFoodDb.Migrations
{
    public partial class SeedTableFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            $"SET IDENTITY_INSERT dbo.Food ON");

            migrationBuilder.Sql(
            $"INSERT INTO dbo.Food(FoodId, FoodName) Values(1, 'Food1')" +
            $"INSERT INTO dbo.Food(FoodId, FoodName) Values(2, 'Food2')");

            migrationBuilder.Sql(
            $"SET IDENTITY_INSERT dbo.Food OFF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            $"DELETE FROM dbo.Food WHERE FoodId <= 2");
        }
    }
}
