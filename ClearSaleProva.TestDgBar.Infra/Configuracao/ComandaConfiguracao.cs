using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearSaleProva.TestDgBar.Infra.Configuracao
{
	public class ComandaConfiguracao : IEntityTypeConfiguration<Comanda>
	{
		public void Configure(EntityTypeBuilder<Comanda> builder)
		{
			builder.ToTable("Comandas");
			builder.HasKey(x => x.Id);

			builder.HasMany(i => i.Itens);

		}
	}
}
