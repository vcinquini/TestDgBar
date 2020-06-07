using Dominio.Entidades;
using Dominio.ObjetosValor;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Servicos
{
	public interface IComandaServico
	{
		Task<Comanda> GetAsync(int id);

		Task<Resultado<Comanda>> AddAsync();

		Task<Resultado<Comanda>> UpdateAsync(Comanda comanda);

		Task<Resultado<Comanda>> CloseComandaAsync(Comanda comanda);

		Task<Resultado<Comanda>> InsertItemAsync(ItemComanda item);

		Task<Resultado<Comanda>> ResetAsync(Comanda comanda);

	}
}
