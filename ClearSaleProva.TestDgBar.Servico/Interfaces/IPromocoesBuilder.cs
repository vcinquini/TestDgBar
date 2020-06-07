using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.Interfaces
{
	public interface IPromocoesBuilder
	{
		Task<Resultado<Comanda>> ObterNotaFiscal(Comanda comanda);
	}
}
