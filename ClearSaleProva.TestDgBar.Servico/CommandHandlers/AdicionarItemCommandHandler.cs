using ClearSaleProva.TestDgBar.Aplicacao.Commands;
using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using Dominio.ObjetosValor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.CommandHandlers
{
	public class AdicionarItemCommandHandler : IRequestHandler<AdicionarItemCommand, Resultado<Comanda>>
	{
		private readonly IComandaServico _comandaServico;

		public AdicionarItemCommandHandler(IComandaServico comandaServico)
		{
			_comandaServico = comandaServico;
		}
		public async Task<Resultado<Comanda>> Handle(AdicionarItemCommand request, CancellationToken cancellationToken)
		{
			ItemComanda it = new ItemComanda()
			{
				ComandaId = request.Id,
				Produto = new Produto(request.ItemId),
				Quantidade = request.Quantidade
			};

			return await _comandaServico.InsertItemAsync(it);
		}
	}
}
