using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClearSaleProva.TestDgBar.Infra.Configuracao
{
	public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
	{
		public void Configure(EntityTypeBuilder<Produto> builder)
		{
			builder.ToTable("Produto");
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Descricao)
					.HasMaxLength(50)
					.HasColumnType("varchar");
		}
	}
}

