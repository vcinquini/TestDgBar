using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Web.Servicos
{
	public class ServicoAutenticacao : IServicoAutenticacao
	{
		private readonly HttpClient _httpClient;
		private readonly SignInManager<CodeChallengeIdentityUser> _signInManager;

		public ServicoAutenticacao(IHttpClientFactory httpClientFactory, SignInManager<CodeChallengeIdentityUser> signInManager)
		{
			_httpClient = httpClientFactory.CreateClient("ServicoAutenticacao");
			_signInManager = signInManager;
		}

		public async Task<ResultadoAutenticacaoViewModel> Authenticate(string login, string senha)
		{
			var content = new StringContent(JsonConvert.SerializeObject(new { login, senha }), Encoding.UTF8, "application/json");
			HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync("account/login", content);

			if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				return Unauthorized();

			string apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
			ResultadoAutenticacaoViewModel apiResponseObject = JsonConvert.DeserializeObject<ResultadoAutenticacaoViewModel>(apiContent);

			var user = new CodeChallengeIdentityUser
			{
				AccessToken = apiResponseObject.AccessToken
			};

			await _signInManager.SignInAsync(user, isPersistent: false);
			return Authorized();
		}

		public Task LogoutAsync()
		{
			return _signInManager.SignOutAsync();
		}

		public ResultadoAutenticacaoViewModel Authorized()
		{
			return new ResultadoAutenticacaoViewModel
			{
				ResultadoAutenticacao = TipoResultadoAutenticacao.Autorizado
			};
		}

		private ResultadoAutenticacaoViewModel Unauthorized()
		{
			return new ResultadoAutenticacaoViewModel
			{
				ResultadoAutenticacao = TipoResultadoAutenticacao.NaoAutorizado
			};
		}
	}

	public class CodeChallengeUserStore : IUserStore<CodeChallengeIdentityUser>
	{
		public Task<IdentityResult> CreateAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<IdentityResult> DeleteAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public void Dispose()
		{
		}

		public Task<CodeChallengeIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<CodeChallengeIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<string> GetNormalizedUserNameAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<string> GetUserIdAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult("admin_id");
		}

		public Task<string> GetUserNameAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			return Task.FromResult("admin");
		}

		public Task SetNormalizedUserNameAsync(CodeChallengeIdentityUser user, string normalizedName, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task SetUserNameAsync(CodeChallengeIdentityUser user, string userName, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}

		public Task<IdentityResult> UpdateAsync(CodeChallengeIdentityUser user, CancellationToken cancellationToken)
		{
			throw new System.NotImplementedException();
		}
	}

}
