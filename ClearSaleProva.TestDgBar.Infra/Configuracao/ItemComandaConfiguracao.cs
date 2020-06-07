using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearSaleProva.TestDgBar.Infra.Configuracao
{
	public class ItemComandaConfiguracao : IEntityTypeConfiguration<ItemComanda>
	{
		public void Configure(EntityTypeBuilder<ItemComanda> builder)
		{
			builder.ToTable("ItemComanda");
			builder.HasKey(x => x.Id);
			builder.HasOne(a => a.Produto);
		}
	}
}

