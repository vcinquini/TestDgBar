using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Servicos
{
	public class ServicoProduto : ServicoBase, IServicoProduto
	{
		public ServicoProduto(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor) :
			base(httpClientFactory, httpContextAccessor)
		{
		}

		public async Task<IEnumerable<ProdutoModel>> GetAllAsync()
		{
			List<ProdutoModel> result;

			var response = await MakeAuthorizedRequestAsync("/produto/lista", HttpMethod.Get);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<List<ProdutoModel>>(json);
			}
			else
				throw new HttpRequestException(response.ReasonPhrase);

			return result;
		}

		public async Task<ProdutoModel> GetByIdAsync(int id)
		{
			var result = new ProdutoModel();
			var response = await MakeAuthorizedRequestAsync($"/produto/{id}", HttpMethod.Get);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				result = JsonConvert.DeserializeObject<ProdutoModel>(json);
			}
			else
				throw new HttpRequestException(response.ReasonPhrase);

			return result;
		}

	}
}
