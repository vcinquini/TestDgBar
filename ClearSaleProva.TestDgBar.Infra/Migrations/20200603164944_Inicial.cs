using Microsoft.EntityFrameworkCore.Migrations;

namespace ClearSaleProva.TestDgBar.Infra.Migrations
{
	public partial class Inicial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "NotaFiscal",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Desconto = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
					ValorTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NotaFiscal", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Produto",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Descricao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
					Preco = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
					PrecPromocional = table.Column<decimal>(type: "decimal(18,4)", nullable: false, defaultValue: 0m)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Produto", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Produto",
				columns: new string[] { "Id", "Descricao", "Preco", "PrecPromocional" },
				values: new object[] { 1, "Cerveja", 5, 3 });

			migrationBuilder.InsertData(
				table: "Produto",
				columns: new string[] { "Id", "Descricao", "Preco" },
				values: new object[] { 2, "Conhaque", 20 });

			migrationBuilder.InsertData(
				table: "Produto",
				columns: new string[] { "Id", "Descricao", "Preco" },
				values: new object[] { 3, "Suco", 50 });

			migrationBuilder.InsertData(
				table: "Produto",
				columns: new string[] { "Id", "Descricao", "Preco" },
				values: new object[] { 4, "Agua", 70 });

		migrationBuilder.CreateTable(
				name: "Comandas",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					NotaFiscalId = table.Column<int>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Comandas", x => x.Id);
					table.ForeignKey(
						name: "FK_Comandas_NotaFiscal_NotaFiscalId",
						column: x => x.NotaFiscalId,
						principalTable: "NotaFiscal",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "ItemComanda",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ComandaId = table.Column<int>(nullable: false),
					ProdutoId = table.Column<int>(nullable: true),
					Quantidade = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ItemComanda", x => x.Id);
					table.ForeignKey(
						name: "FK_ItemComanda_Comandas_ComandaId",
						column: x => x.ComandaId,
						principalTable: "Comandas",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_ItemComanda_Produto_ProdutoId",
						column: x => x.ProdutoId,
						principalTable: "Produto",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Comandas_NotaFiscalId",
				table: "Comandas",
				column: "NotaFiscalId");

			migrationBuilder.CreateIndex(
				name: "IX_ItemComanda_ComandaId",
				table: "ItemComanda",
				column: "ComandaId");

			migrationBuilder.CreateIndex(
				name: "IX_ItemComanda_ProdutoId",
				table: "ItemComanda",
				column: "ProdutoId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ItemComanda");

			migrationBuilder.DropTable(
				name: "Comandas");

			migrationBuilder.DropTable(
				name: "Produto");

			migrationBuilder.DropTable(
				name: "NotaFiscal");
		}
	}
}
