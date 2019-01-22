using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QAFoodDb.Migrations
{
    public partial class createtablereviewdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewDetails",
                columns: table => new
                {
                    ReviewDetailsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlavourScore = table.Column<int>(nullable: false),
                    PresentationScore = table.Column<int>(nullable: false),
                    AromaScore = table.Column<int>(nullable: false),
                    TextureScore = table.Column<int>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewDetails", x => x.ReviewDetailsId);
                    table.ForeignKey(
                        name: "FK_ReviewDetails_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewDetails_ReviewId",
                table: "ReviewDetails",
                column: "ReviewId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewDetails");
        }
    }
}
