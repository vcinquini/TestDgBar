using ClearSaleProva.TestDgBar.Aplicacao.Commands;
using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Aplicacao.CommandHandlers
{
	public class AbrirComandaCommandHandler : IRequestHandler<AbrirComandaCommand, Resultado<Comanda>>
	{
		private readonly IComandaServico _comandaServico;

		public AbrirComandaCommandHandler(IComandaServico comandaServico)
		{
			_comandaServico = comandaServico;
		}

		public Task<Resultado<Comanda>> Handle(AbrirComandaCommand request, CancellationToken cancellationToken)
		{
			return _comandaServico.AddAsync();
		}
	}
}
