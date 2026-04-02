using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceSqlSolutions.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_AgentId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgentId_CreateDate",
                table: "Orders",
                columns: new[] { "AgentId", "CreateDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_Name",
                table: "Goods",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_GoodProperties_Bdate_Edate",
                table: "GoodProperties",
                columns: new[] { "Bdate", "Edate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_AgentId_CreateDate",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Goods_Name",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_GoodProperties_Bdate_Edate",
                table: "GoodProperties");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgentId",
                table: "Orders",
                column: "AgentId");
        }
    }
}
