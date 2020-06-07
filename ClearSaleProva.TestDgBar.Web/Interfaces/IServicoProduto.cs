using ClearSaleProva.TestDgBar.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Interfaces
{
	public interface IServicoProduto
	{
		Task<IEnumerable<ProdutoModel>> GetAllAsync();
		Task<ProdutoModel> GetByIdAsync(int id);
	}
}