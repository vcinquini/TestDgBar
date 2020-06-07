using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Controllers
{
	[Authorize]
	public class ComandaController : Controller
	{
		private readonly ILogger<ComandaController> _logger;
		private readonly IServicoComanda _servicoComanda;
		private readonly IServicoProduto _servicoProduto;

		public ComandaController(ILogger<ComandaController> logger, IServicoComanda servicoComanda, IServicoProduto servicoProduto)
		{
			_logger = logger;
			_servicoComanda = servicoComanda;
			_servicoProduto = servicoProduto;
		}

		public IActionResult Index()
		{
			ViewBag.Title = "ClearSaleProva.TestDgBar";
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> AbrirComanda()
		{
			var model = await _servicoComanda.AbrirComandaAsync();
			ViewBag.Title = $"ClearSaleProva.TestDgBar - Comanda #{model.Id}";

			if(model.Id == 0)
				model.Mensagem = "Não foi possível abrir uma comanda";

			model.Produtos = await GetListaProdutos();
			return View("Comanda", model);
		}

		[HttpGet]
		public async Task<IActionResult> ResetarComanda(int id)
		{
			var model = await _servicoComanda.ResetarComandaAsync(id);
			model.Produtos = await GetListaProdutos();
			return View("Comanda", model);
		}

		[HttpGet]
		public async Task<IActionResult> FecharComanda(int id)
		{
			var model = await _servicoComanda.FecharComandaAsync(id);
			return View("Fechamento", model);
		}

		[HttpPost]
		public async Task<IActionResult> InserirItem(ComandaViewModel request)
		{
			if (ModelState.IsValid)
			{
				ItemComandaViewModel model = new ItemComandaViewModel()
				{
					Id = request.Id,
					ItemId = request.ItemId,
					Quantidade = request.Quantidade
				};

				var ret = await _servicoComanda.AdicionarItemComandaAsync(model);

				if (ret.Ok)
				{
					ComandaViewModel comanda = await _servicoComanda.GetByIdAsync(request.Id);
					comanda.Produtos = await GetListaProdutos();
					return View("Comanda", comanda);
				}
				else
				{
					request.Mensagem = ret.CodigoErro;
				}

			}
			// repopula a lista de produtos
			request.Produtos = await GetListaProdutos();
			return View("Comanda", request);

		}

		private async Task<SelectList> GetListaProdutos()
		{
			try
			{
				var lista = await _servicoProduto.GetAllAsync();
				return new SelectList(lista, "Id", "Descricao");
			}
			catch (HttpRequestException)
			{
				IEnumerable<ProdutoModel> tmp = new List<ProdutoModel>();
				return new SelectList(tmp, "Id", "Descricao");
			}		
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


	}
}
