using ClearSaleProva.TestDgBar.Web.Models;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Interfaces
{
	public interface IServicoComanda
	{
		Task<ComandaViewModel> GetByIdAsync(int id);
		Task<ComandaViewModel> AbrirComandaAsync();
		Task<FechamentoViewModel> FecharComandaAsync(int id);
		Task<ComandaViewModel> ResetarComandaAsync(int id);
		Task<Resultado> AdicionarItemComandaAsync(ItemComandaViewModel model);
	}
}