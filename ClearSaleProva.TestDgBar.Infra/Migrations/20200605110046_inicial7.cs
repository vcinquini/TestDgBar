using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class inicial7 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Comandas_NotaFiscal_NotaFiscalId",
				table: "Comandas");

			migrationBuilder.DropTable(
				name: "NotaFiscal");

			migrationBuilder.DropIndex(
				name: "IX_Comandas_NotaFiscalId",
				table: "Comandas");

			migrationBuilder.DropColumn(
				name: "NotaFiscalId",
				table: "Comandas");

			migrationBuilder.AddColumn<decimal>(
				name: "Desconto",
				table: "Comandas",
				type: "decimal(18,4)",
				nullable: false,
				defaultValue: 0m);

			migrationBuilder.AddColumn<string>(
				name: "Mensagem",
				table: "Comandas",
				type: "varchar(500)",
				nullable: true);

			migrationBuilder.AddColumn<decimal>(
				name: "ValorComanda",
				table: "Comandas",
				type: "decimal(18,4)",
				nullable: false,
				defaultValue: 0m);

			migrationBuilder.AddColumn<decimal>(
				name: "ValorTotal",
				table: "Comandas",
				type: "decimal(18,4)",
				nullable: false,
				defaultValue: 0m);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Desconto",
				table: "Comandas");

			migrationBuilder.DropColumn(
				name: "Mensagem",
				table: "Comandas");

			migrationBuilder.DropColumn(
				name: "ValorComanda",
				table: "Comandas");

			migrationBuilder.DropColumn(
				name: "ValorTotal",
				table: "Comandas");

			migrationBuilder.AddColumn<int>(
				name: "NotaFiscalId",
				table: "Comandas",
				type: "int",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "NotaFiscal",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Desconto = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
					Mensagem = table.Column<string>(type: "varchar(500", nullable: true),
					ValorComanda = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
					ValorTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NotaFiscal", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comandas_NotaFiscalId",
				table: "Comandas",
				column: "NotaFiscalId");

			migrationBuilder.AddForeignKey(
				name: "FK_Comandas_NotaFiscal_NotaFiscalId",
				table: "Comandas",
				column: "NotaFiscalId",
				principalTable: "NotaFiscal",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
