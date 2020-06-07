using ClearSaleProva.TestDgBar.Aplicacao.Commands;
using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.CommandHandlers
{
	public class FecharComandaCommandHandler : IRequestHandler<FecharComandaCommand, Resultado<Comanda>>
	{
		private readonly IComandaServico _comandaServico;
		private readonly IPromocoesBuilder _promoBuilder;

		public FecharComandaCommandHandler(IComandaServico comandaServico, IPromocoesBuilder promoBuilder)
		{
			_comandaServico = comandaServico;
			_promoBuilder = promoBuilder;
		}

		public async Task<Resultado<Comanda>> Handle(FecharComandaCommand request, CancellationToken cancellationToken)
		{
			var comanda = await _comandaServico.GetAsync(request.ComandaId);
			if (comanda == null)
				return Resultado<Comanda>.Falha("Comanda não encontrada");

			Resultado<Comanda> res = await _promoBuilder.ObterNotaFiscal(comanda);

			if (!res.Ok)
				return res;

			return await _comandaServico.CloseComandaAsync(res.Value);
		}
	}
}
