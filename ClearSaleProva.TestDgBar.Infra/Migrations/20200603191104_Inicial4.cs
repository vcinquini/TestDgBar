using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class Inicial4 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Mensagem",
				table: "NotaFiscal",
				type: "varchar(500)",
				nullable: true,
				oldClrType: typeof(decimal),
				oldType: "varchar(500");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<decimal>(
				name: "Mensagem",
				table: "NotaFiscal",
				type: "varchar(500)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "varchar(500)",
				oldNullable: true);
		}
	}
}
