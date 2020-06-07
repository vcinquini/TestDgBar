using Dominio.ObjetosValor;
using MediatR;
using System.Collections.Generic;

namespace ClearSaleProva.TestDgBar.Aplicacao.Queries
{
	public sealed class ListaProdutosQuery : IRequest<IReadOnlyList<Produto>>
	{
		public ListaProdutosQuery()
		{
		}
	}
}
