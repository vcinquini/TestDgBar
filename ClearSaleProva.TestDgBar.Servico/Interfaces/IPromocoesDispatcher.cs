using Dominio.ObjetosValor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.Interfaces
{
	public interface IPromocoesDispatcher
	{
		Task PromocoesDispatch(List<ItemComanda> lista);
	}
}