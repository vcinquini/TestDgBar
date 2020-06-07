using ClearSaleProva.TestDgBar.Identity.Model;

namespace ClearSaleProva.TestDgBar.Identity.Servicos
{
    public interface IServicoAutenticacao
    {
        AutenticacaoResultViewModel Authenticate(string username, string password);
    }
}
