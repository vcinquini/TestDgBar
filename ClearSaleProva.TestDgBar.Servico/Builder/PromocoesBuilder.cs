using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using System.Threading.Tasks;

namespace Aplicacao.Builder
{
	public class PromocoesBuilder : IPromocoesBuilder
	{
		private readonly IBuilder _builder;
		private readonly IPromocoesDispatcher _dispatcher;

		public PromocoesBuilder(IBuilder builder, IPromocoesDispatcher dispatcher)
		{
			_builder = builder;
			_dispatcher = dispatcher;
		}

		public async Task<Resultado<Comanda>> ObterNotaFiscal(Comanda comanda)
		{
			// constroi a lista de todos os investimentos
			await _dispatcher.PromocoesDispatch(comanda.Itens);

			//obtem a nf calculada
			comanda.ValorComanda = _builder.NotaFiscal.ValorComanda;
			comanda.ValorTotal = _builder.NotaFiscal.ValorTotal;
			comanda.Desconto = _builder.NotaFiscal.Desconto;
			comanda.Mensagem = _builder.NotaFiscal.Mensagem;

			return Resultado<Comanda>.Sucesso(comanda);
		}
	}
}
