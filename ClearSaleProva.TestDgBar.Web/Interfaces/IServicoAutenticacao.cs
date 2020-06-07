using ClearSaleProva.TestDgBar.Web.Models;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Interfaces
{
	public interface IServicoAutenticacao
	{
		Task<ResultadoAutenticacaoViewModel> Authenticate(string username, string password);
		Task LogoutAsync();
	}
}
