using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelleryShop.DataAccess.Migrations
{
    public partial class AddMultiplierTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "PriceMultipliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyMultiplier = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SellMultiplier = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceMultipliers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceMultipliers");

        }
    }
}
