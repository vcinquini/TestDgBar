using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using MediatR;

namespace ClearSaleProva.TestDgBar.Aplicacao.Commands
{
	public class AdicionarItemCommand : IRequest<Resultado<Comanda>>
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		public int Quantidade { get; set; }

		public AdicionarItemCommand()
		{

		}

		public AdicionarItemCommand(int itemId, int comandaId, int quantidade)
		{
			Id = comandaId;
			ItemId = itemId;
			Quantidade = quantidade;
		}
	}
}
