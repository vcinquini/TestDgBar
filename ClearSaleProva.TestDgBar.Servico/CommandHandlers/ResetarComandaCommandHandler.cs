using ClearSaleProva.TestDgBar.Aplicacao.Commands;
using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.CommandHandlers
{
	public class ResetarComandaCommandHandler : IRequestHandler<ResetarComandaCommand, Resultado<Comanda>>
	{
		private readonly IComandaServico _comandaServico;

		public ResetarComandaCommandHandler(IComandaServico comandaServico)
		{
			_comandaServico = comandaServico;
		}

		public async Task<Resultado<Comanda>> Handle(ResetarComandaCommand request, CancellationToken cancellationToken)
		{
			Comanda comanda = new Comanda()
			{
				Id = request.ComandaId
			};

			return await _comandaServico.ResetAsync(comanda);

		}
	}
}
