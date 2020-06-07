using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using Dominio.ObjetosValor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class ProdutoController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ProdutoController> _logger;

		public ProdutoController(IMediator mediator, ILogger<ProdutoController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int Id)
		{
			_logger.LogInformation("Get request iniciado.");

			ProdutosQuery query = new ProdutosQuery(Id);

			Produto produto = await _mediator.Send(query);

			if (produto == null)
			{
				_logger.LogInformation($"Produto {Id} não encontrado");
				return NotFound();
			}

			_logger.LogInformation("Get request finalizado");
			return Ok(produto);
		}



		[HttpGet()]
		[Route("lista")]
		public async Task<IActionResult> ObterListaProdutos()
		{
			_logger.LogInformation("ObterListaProdutos request iniciado.");

			IReadOnlyList<Produto> produtos = await _mediator.Send(new ListaProdutosQuery());

			if (produtos == null)
			{
				_logger.LogInformation("Lista de Produtos não encontrado");
				return NotFound();
			}

			_logger.LogInformation("ObterListaProdutos request finalizado");
			return Ok(produtos);
		}
	}
}
