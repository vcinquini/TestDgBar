using ClearSaleProva.TestDgBar.Identity.Model;
using ClearSaleProva.TestDgBar.Identity.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClearSaleProva.TestDgBar.Identity.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{

		private readonly ILogger<AccountController> _logger;
		private readonly IServicoAutenticacao _servicoAutenticacao;

		public AccountController(ILogger<AccountController> logger, IServicoAutenticacao servicoAutenticacao)
		{
			_logger = logger;
			_servicoAutenticacao = servicoAutenticacao;
		}

		[HttpPost]
		[Route("login")]
		public IActionResult Login(AutenticacaoRequestViewModel viewModel)
		{
			AutenticacaoResultViewModel result = _servicoAutenticacao.Authenticate(viewModel.Login, viewModel.Senha);

			if (result.ResultadoAutenticacao == StatusAutenticacao.NaoAutorizado)
			{
				Response.Headers.Add("WWW-Authenticate", "Basic");
				return Unauthorized(result);
			}

			return Ok(result);
		}

	}
}
