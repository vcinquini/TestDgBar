using Dominio.ObjetosValor;
using MediatR;

namespace ClearSaleProva.TestDgBar.Aplicacao.Queries
{
	public sealed class ProdutosQuery : IRequest<Produto>
	{
		public int Id { get; internal set; }

		public ProdutosQuery(int id)
		{
			Id = id;
		}

	}
}
