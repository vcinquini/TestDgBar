
namespace ClearSaleProva.TestDgBar.Identity.Model
{
    public class AutenticacaoResultViewModel
    {
        public StatusAutenticacao ResultadoAutenticacao { get; set; }
        public string AccessToken { get; set; }
    }

    public enum StatusAutenticacao
    {
        Autorizado = 1,
        NaoAutorizado = 2
    }
}
