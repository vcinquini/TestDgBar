using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.ObjetosValor;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.QueryHandlers
{
	public class ListaProdutosQueryHandler : IRequestHandler<ListaProdutosQuery, IReadOnlyList<Produto>>
	{
		private readonly IProdutoServico _produtoServico;

		public ListaProdutosQueryHandler(IProdutoServico produtoServico)
		{
			_produtoServico = produtoServico;
		}


		public async Task<IReadOnlyList<Produto>> Handle(ListaProdutosQuery request, CancellationToken cancellationToken)
		{
			return await _produtoServico.GetListAsync();
		}
	}
}
