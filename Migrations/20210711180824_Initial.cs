using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory1",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    inventoryNumber = table.Column<string>(type: "TEXT", nullable: true),
                    inventoryName = table.Column<string>(type: "TEXT", nullable: true),
                    inventoryPrice = table.Column<double>(type: "REAL", nullable: false),
                    inventoryQuantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory1", x => x.InventoryID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    productName = table.Column<string>(type: "TEXT", nullable: true),
                    productPrice = table.Column<double>(type: "REAL", nullable: false),
                    productQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    total = table.Column<double>(type: "REAL", nullable: false),
                    subTotal = table.Column<double>(type: "REAL", nullable: false),
                    taxes = table.Column<double>(type: "REAL", nullable: false),
                    allTotal = table.Column<double>(type: "REAL", nullable: false),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrdersId);
                    table.ForeignKey(
                        name: "FK_Order_Inventory1_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory1",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_InventoryID",
                table: "Order",
                column: "InventoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Inventory1");
        }
    }
}
