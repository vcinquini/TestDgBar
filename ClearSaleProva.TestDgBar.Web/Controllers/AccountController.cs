using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IServicoAutenticacao _servicoAutenticacao;

		public AccountController(IServicoAutenticacao authenticationService)
		{
			_servicoAutenticacao = authenticationService;
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await _servicoAutenticacao.LogoutAsync();
			return RedirectToAction("Login");
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequestViewModel request, string returnUrl)
		{
			ResultadoAutenticacaoViewModel result = await _servicoAutenticacao.Authenticate(request.Login, request.Senha);

			if (result.ResultadoAutenticacao == TipoResultadoAutenticacao.NaoAutorizado)
			{
				ModelState.AddModelError(string.Empty, "Usuário ou Senha Inválido");
				return View();
			}

			if (!string.IsNullOrWhiteSpace(returnUrl))
				return LocalRedirect(returnUrl);

			return RedirectToAction("Index", "Comanda");
		}
	}
}