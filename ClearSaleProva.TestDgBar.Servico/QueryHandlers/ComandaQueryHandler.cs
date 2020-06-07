using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.QueryHandlers
{
	public class ComandaQueryHandler : IRequestHandler<ComandaQuery, Comanda>
	{
		private readonly IComandaServico _comandaServico;

		public ComandaQueryHandler(IComandaServico comandaServico)
		{
			_comandaServico = comandaServico;
		}


		public async Task<Comanda> Handle(ComandaQuery request, CancellationToken cancellationToken)
		{
			return await _comandaServico.GetAsync(request.Id);
		}
	}
}
