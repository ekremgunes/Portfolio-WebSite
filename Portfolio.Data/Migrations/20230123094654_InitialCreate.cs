using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contentName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rank = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "99"),
                    InShowCase = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    senderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senderMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    messageContent = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    isRead = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rank = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "99"),
                    isActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sliderImgPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sliderHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sliderServices = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aboutContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aboutHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    aboutImgPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slogan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timedMessage = table.Column<bool>(type: "bit", nullable: true),
                    instagramUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instagramVisibility = table.Column<bool>(type: "bit", nullable: true),
                    twitterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    twitterVisibility = table.Column<bool>(type: "bit", nullable: true),
                    facebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    facebookVisibility = table.Column<bool>(type: "bit", nullable: true),
                    seo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skillPercent = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)20),
                    rank = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "99")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ContentDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contentDetailName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rank = table.Column<short>(type: "smallint", nullable: false, defaultValueSql: "99"),
                    contentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_ContentDetails_Contents_contentId",
                        column: x => x.contentId,
                        principalTable: "Contents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imgPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contentDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.id);
                    table.ForeignKey(
                        name: "FK_Images_ContentDetails_contentDetailId",
                        column: x => x.contentDetailId,
                        principalTable: "ContentDetails",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "id", "aboutContent", "aboutHeader", "aboutImgPath", "facebookUrl", "facebookVisibility", "instagramUrl", "instagramVisibility", "seo", "sliderHeader", "sliderImgPath", "sliderServices", "slogan", "timedMessage", "twitterUrl", "twitterVisibility" },
                values: new object[] { 1, "about me text", "aboutheader", "imgpathabout", "facebook.com", true, "instagram.com", true, "<meta description='Perfect website'>", "slider header", "sliderimgpath", "service area", "just do it", true, "twitter.com", true });

            migrationBuilder.CreateIndex(
                name: "IX_ContentDetails_contentId",
                table: "ContentDetails",
                column: "contentId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_contentDetailId",
                table: "Images",
                column: "contentDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "ContentDetails");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
