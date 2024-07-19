using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JewelleryShop.DataAccess.Migrations
{
    public partial class AddCustomerCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Customer",
                type: "datetime2",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Customer");

        }
    }
}
