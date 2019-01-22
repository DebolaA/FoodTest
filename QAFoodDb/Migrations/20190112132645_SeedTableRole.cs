using Microsoft.EntityFrameworkCore.Migrations;

namespace QAFoodDb.Migrations
{
    public partial class SeedTableRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            $"SET IDENTITY_INSERT dbo.Role ON");

            migrationBuilder.Sql(
            $"INSERT INTO dbo.Role(RoleId, RoleName) Values(1, 'Admin')" +
            $"INSERT INTO dbo.Role(RoleId, RoleName) Values(2, 'FoodTester')");

            migrationBuilder.Sql(
            $"SET IDENTITY_INSERT dbo.Role OFF" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            $"Delete FROM dbo.Role WHERE RoleId = 1" +
            $"Delete FROM dbo.Role WHERE RoleId = 2");
        }
    }
}
