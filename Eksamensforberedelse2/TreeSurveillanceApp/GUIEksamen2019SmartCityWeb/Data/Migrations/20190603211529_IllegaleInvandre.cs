using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GUIEksamen2019SmartCityWeb.Data.Migrations
{
    public partial class IllegaleInvandre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationModel",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    ListOfTrees = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModel", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "TreeSensorModel",
                columns: table => new
                {
                    SensorID = table.Column<string>(nullable: false),
                    TreeSort = table.Column<string>(nullable: false),
                    LocationID = table.Column<int>(nullable: false),
                    CoordinateIat = table.Column<double>(nullable: false),
                    CoordinateIon = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeSensorModel", x => x.SensorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationModel");

            migrationBuilder.DropTable(
                name: "TreeSensorModel");
        }
    }
}
