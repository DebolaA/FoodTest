using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QAFoodDb.Migrations
{
    public partial class tbluser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "CreatedBy", "CreatedOn", "Email", "FName", "LName", "RoleId" },
                values: new object[] { 1, null, null, "user@yahoo.com", null, null, null });

            migrationBuilder.InsertData(
             table: "User",
             columns: new[] { "Email" },
             values: new object[] { "dee@yahoo.com" });
                }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "User",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
