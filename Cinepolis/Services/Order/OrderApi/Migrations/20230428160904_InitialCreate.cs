using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrderApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerName", "UpdatedAt" },
                values: new object[,]
                {
                    { "_6dcd9756-6568-4d23-8d03-12013b34ca27-1394ACB3D54D7D1528983FED925EC9AC", new DateTime(2023, 4, 28, 18, 9, 4, 880, DateTimeKind.Local).AddTicks(5443), null, null },
                    { "_6dcd9756-6568-4d23-8d03-12013b34ca27-727879562184A82CAC08A403F1F78D31", new DateTime(2023, 4, 28, 18, 9, 4, 880, DateTimeKind.Local).AddTicks(5500), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
