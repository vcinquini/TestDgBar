using ClearSaleProva.TestDgBar.Infra.Configuracao;
using Dominio.Entidades;
using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;

namespace ClearSaleProva.TestDgBar.InfraEstrutura
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Comanda> Comandas { get; set; }
		public DbSet<Produto> Produtos { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ComandaConfiguracao());
			modelBuilder.ApplyConfiguration(new ProdutoConfiguracao());
			modelBuilder.ApplyConfiguration(new ItemComandaConfiguracao());

			base.OnModelCreating(modelBuilder);
		}

	}
}
