using ClearSaleProva.TestDgBar.Aplicacao.Commands;
using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using ClearSaleProva.TestDgBar.Dominio;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Api.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class ComandaController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ILogger<ComandaController> _logger;

		public ComandaController(IMediator mediator, ILogger<ComandaController> logger)
		{
			_mediator = mediator;
			_logger = logger;
		}

		[HttpGet("{id}", Name = "get")]
		public async Task<IActionResult> GetById(int Id)
		{
			_logger.LogInformation("Get request iniciado.");

			if (Id == 0)
			{
				_logger.LogInformation($"Id de Comanda inválida");
				return BadRequest();
			}

			ComandaQuery query = new ComandaQuery(Id);

			Comanda comanda = await _mediator.Send(query);

			if (comanda == null)
			{
				_logger.LogInformation($"Comanda {Id} não encontrado");
				return NotFound();
			}
			_logger.LogInformation("Get request finalizado");
			return Ok(comanda);
		}


		[HttpPost()]
		[Route("abrircomanda")]
		public async Task<IActionResult> AbrirComanda()
		{
			_logger.LogInformation("AbrirComanda request iniciado.");

			AbrirComandaCommand comando = new AbrirComandaCommand();

			Resultado<Comanda> resultado = await _mediator.Send(comando);

			if (!resultado.Ok)
			{
				_logger.LogInformation("Erro ao abrir comanda");
				return Problem(detail: resultado.CodigoErro, statusCode: 500);
			}

			_logger.LogInformation("AbrirComanda request finalizado");
			return Ok(resultado.Value);
		}


		[HttpPost()]
		[Route("fecharcomanda/{id}")]
		public async Task<IActionResult> FecharComanda(int id)
		{
			_logger.LogInformation("FecharComanda request iniciado.");

			FecharComandaCommand comando = new FecharComandaCommand(id);

			Resultado<Comanda> resultado = await _mediator.Send(comando);

			if (!resultado.Ok)
			{
				_logger.LogInformation("Erro ao fechar comanda");
				return Problem(detail: resultado.CodigoErro, statusCode: 500);
			}

			_logger.LogInformation("FecharComanda request finalizado");
			return Ok(resultado.Value);
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> ResetarComanda(int id)
		{
			_logger.LogInformation("ResetarComanda request iniciado.");

			ResetarComandaCommand comando = new ResetarComandaCommand(id);

			Resultado<Comanda> resultado = await _mediator.Send(comando);

			if (!resultado.Ok)
			{
				_logger.LogInformation("Erro ao resetar comanda");
				return Problem(detail: resultado.CodigoErro, statusCode: 500);
			}

			_logger.LogInformation("ResetarComanda request finalizado");
			return Ok(resultado.Value);
		}


		[HttpPut()]
		public async Task<IActionResult> AdicionarItemComanda([FromBody] AdicionarItemCommand request)
		{
			_logger.LogInformation("AdicionarItemComanda request iniciado.");

			Resultado<Comanda> resultado = await _mediator.Send(request);

			if (!resultado.Ok)
			{
				_logger.LogInformation("Erro ao adicionar item na comanda");
				return Problem(detail: resultado.CodigoErro, statusCode: 500);
			}

			_logger.LogInformation("AdicionarItemComanda request finalizado");
			return Ok(resultado.Value);
		}
	}
}
