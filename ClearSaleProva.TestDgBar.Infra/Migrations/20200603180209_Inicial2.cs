using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class Inicial2 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<decimal>(
				name: "Mensagem",
				table: "NotaFiscal",
				type: "varchar(500)",
				nullable: false,
				defaultValue: 0m);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Mensagem",
				table: "NotaFiscal");
		}
	}
}
