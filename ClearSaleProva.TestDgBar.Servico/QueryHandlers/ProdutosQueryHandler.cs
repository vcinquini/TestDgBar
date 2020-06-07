using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.ObjetosValor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.QueryHandlers
{
	public class ProdutosQueryHandler : IRequestHandler<ProdutosQuery, Produto>
	{
		private readonly IProdutoServico _produtoServico;

		public ProdutosQueryHandler(IProdutoServico produtoServico)
		{
			_produtoServico = produtoServico;
		}


		public async Task<Produto> Handle(ProdutosQuery request, CancellationToken cancellationToken)
		{
			return await _produtoServico.GetAsync(request.Id);
		}
	}
}
