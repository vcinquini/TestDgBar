using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using Dominio.ObjetosValor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Builder
{
	public class PromocoesDispatcher : IPromocoesDispatcher
	{
		private readonly IBuilder _builder;

		public PromocoesDispatcher(IBuilder builder)
		{
			_builder = builder;
		}

		public async Task PromocoesDispatch(List<ItemComanda> lista)
		{
			await Task.Run(() =>
			{
				_builder.Lista = lista;
				_builder.CalcularNf();
				_builder.PromocaoCervejaSuco();
				_builder.PromocaoConhaqueCerveja();
			});
		}
	}
}
