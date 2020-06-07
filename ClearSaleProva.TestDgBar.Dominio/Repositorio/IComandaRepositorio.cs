using Dominio.Entidades;
using Dominio.ObjetosValor;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Repositorio
{
	public interface IComandaRepositorio
	{
		Task<Comanda> GetAsync(int id);

		Task<Comanda> AddAsync(Comanda comanda);

		Task UpdateAsync(Comanda comanda);

		Task ResetAsync(Comanda comanda);

		Task InsertItemAsync(ItemComanda item);
	}
}
