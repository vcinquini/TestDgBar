using Dominio.ObjetosValor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Repositorio
{
	public interface IProdutoRepositorio
	{
		Task<Produto> GetAsync(int id);

		Task<List<Produto>> GetListAsync();
	}
}
