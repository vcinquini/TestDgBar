using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using MediatR;

namespace ClearSaleProva.TestDgBar.Aplicacao.Commands
{
	public class FecharComandaCommand : IRequest<Resultado<Comanda>>
	{
		public int ComandaId { get; set; }

		public FecharComandaCommand(int id)
		{
			ComandaId = id;
		}
	}
}
