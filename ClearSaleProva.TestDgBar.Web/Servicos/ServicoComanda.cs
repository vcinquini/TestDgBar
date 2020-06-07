using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Servicos
{
	public class ServicoComanda : ServicoBase, IServicoComanda
	{
		public ServicoComanda(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
			base(httpClientFactory, httpContextAccessor)
		{
		}

		public async Task<ComandaViewModel> GetByIdAsync(int id)
		{
			var result = new ComandaViewModel();
			var response = await MakeAuthorizedRequestAsync($"/comanda/{id}", HttpMethod.Get);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<ComandaViewModel>(json);
			}

			return result;
		}


		public async Task<ComandaViewModel> AbrirComandaAsync()
		{
			var result = new ComandaViewModel();
			var response = await MakeAuthorizedRequestAsync($"/comanda/abrircomanda", HttpMethod.Post);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<ComandaViewModel>(json);
			}
			else
			{
				result.Mensagem = $"{response.StatusCode} - {response.ReasonPhrase}";
			}
			return result;
		}

		public async Task<FechamentoViewModel> FecharComandaAsync(int id)
		{
			var result = new FechamentoViewModel();
			var response = await MakeAuthorizedRequestAsync($"/comanda/fecharcomanda/{id}", HttpMethod.Post);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<FechamentoViewModel>(json);
			}

			return result;
		}

		public async Task<ComandaViewModel> ResetarComandaAsync(int id)
		{
			var result = new ComandaViewModel();
			var response = await MakeAuthorizedRequestAsync($"/comanda/{id}", HttpMethod.Delete);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<ComandaViewModel>(json);
			}

			return result;
		}

		public async Task<Resultado> AdicionarItemComandaAsync(ItemComandaViewModel model)
		{
			var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
			var response = await MakeAuthorizedRequestAsync($"/comanda/", HttpMethod.Put, stringContent);
			return await CreateResultFromApiResponse(response);
		}


	}
}
