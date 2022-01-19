using Microsoft.EntityFrameworkCore.Migrations;

namespace webApi.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Stu_ID = table.Column<string>(nullable: false),
                    Crs_Seq = table.Column<string>(nullable: true),
                    CrsName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Timestamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Stu_ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Stu_ID = table.Column<string>(nullable: false),
                    Stu_Seq = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Tch_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Stu_ID);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Tch_ID = table.Column<string>(nullable: false),
                    Tch_Seq = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Major = table.Column<string>(nullable: true),
                    Timestamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Tch_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
