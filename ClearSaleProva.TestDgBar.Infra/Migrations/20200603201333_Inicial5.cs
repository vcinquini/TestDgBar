using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class Inicial5 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<decimal>(
				name: "ValorComanda",
				table: "NotaFiscal",
				type: "decimal(18,4)",
				nullable: false,
				defaultValue: 0m);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "ValorComanda",
				table: "NotaFiscal");
		}
	}
}
