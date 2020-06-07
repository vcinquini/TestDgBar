using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using MediatR;

namespace ClearSaleProva.TestDgBar.Aplicacao.Commands
{
	public class AbrirComandaCommand : IRequest<Resultado<Comanda>>
	{
		public AbrirComandaCommand()
		{
		}

	}
}
