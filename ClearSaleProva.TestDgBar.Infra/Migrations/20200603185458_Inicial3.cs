using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class Inicial3 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
	

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "PrecPromocional",
				table: "Produto");
		}
	}
}
