using Dominio.Entidades;
using MediatR;


namespace ClearSaleProva.TestDgBar.Aplicacao.Queries
{
	public sealed class ComandaQuery : IRequest<Comanda>
	{
		public int Id { get; set; }

		public ComandaQuery(int id)
		{
			Id = id;
		}
	}
}
