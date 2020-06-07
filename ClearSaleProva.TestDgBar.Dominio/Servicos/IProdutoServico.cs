using Dominio.ObjetosValor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Servicos
{
	public interface IProdutoServico
	{

		Task<Produto> GetAsync(int id);

		Task<IReadOnlyList<Produto>> GetListAsync();
	}
}
