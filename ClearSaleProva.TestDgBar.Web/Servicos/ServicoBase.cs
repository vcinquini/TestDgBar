using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ClearSaleProva.TestDgBar.Web.Servicos
{
	public abstract class ServicoBase
	{
		protected readonly HttpClient _httpHttpClient;
		protected readonly IHttpContextAccessor _httpContextAccessor;

		protected ServicoBase(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
		{
			_httpHttpClient = httpClientFactory.CreateClient("ServicoComanda");
			_httpContextAccessor = httpContextAccessor;
		}

		protected async Task<Resultado> CreateResultFromApiResponse(HttpResponseMessage response)
		{
			string content = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				var result = JsonConvert.DeserializeObject<ApiResultado>(content);

				if (!result.EhSucesso)
				{
					return Resultado.Falha(result.CodigoErro);
				}
			}

			return Resultado.Sucesso();
		}

		protected Task<HttpResponseMessage> MakeAuthorizedRequestAsync(string endpoint, HttpMethod method, HttpContent content = null)
		{
			var requestMessage = new HttpRequestMessage(method, endpoint);
			requestMessage.Content = content;

			requestMessage.Headers.Authorization = new System.Net.Http.Headers
				.AuthenticationHeaderValue("Bearer", _httpContextAccessor.HttpContext.User
					.Claims.First(c => c.Type == CustomUserClaims.BearerTokenClaimType).Value);

			return _httpHttpClient.SendAsync(requestMessage);
		}

	}
}
