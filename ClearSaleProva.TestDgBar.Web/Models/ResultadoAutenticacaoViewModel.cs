
namespace ClearSaleProva.TestDgBar.Web.Models
{
	public class ResultadoAutenticacaoViewModel
	{
		public TipoResultadoAutenticacao ResultadoAutenticacao { get; set; }
		public string AccessToken { get; set; }
	}

	public enum TipoResultadoAutenticacao
	{
		Autorizado = 1,
		NaoAutorizado = 2
	}
}
