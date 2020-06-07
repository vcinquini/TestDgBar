using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using MediatR;

namespace ClearSaleProva.TestDgBar.Aplicacao.Commands
{
	public class ResetarComandaCommand : IRequest<Resultado<Comanda>>
	{
		public int ComandaId { get; set; }

		public ResetarComandaCommand(int id)
		{
			ComandaId = id;
		}
	}
}
