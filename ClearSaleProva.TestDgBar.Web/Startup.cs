using ClearSaleProva.TestDgBar.Web.Interfaces;
using ClearSaleProva.TestDgBar.Web.Models;
using ClearSaleProva.TestDgBar.Web.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Globalization;
using System.Net.Http;

namespace ClearSaleProva.TestDgBar.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			LoggerFactory loggerFactory = new LoggerFactory();
			var _logger = loggerFactory.CreateLogger<Startup>();

			services.AddControllersWithViews();

			var retryPolicy = Policy
			   .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
			   .RetryAsync(3, onRetry: (message, retryCount) =>
			   {
				   LoggerFactory loggerFactory = new LoggerFactory();
				   var _logger = loggerFactory.CreateLogger<Startup>();
				   _logger.LogDebug($"Tentativa #{retryCount} - Motivo: {message}");
			   });

			services.AddHttpClient("ServicoComanda", x => x.BaseAddress = new Uri("http://localhost:5000"))
				.AddPolicyHandler(retryPolicy);
			services.AddHttpClient("ServicoAutenticacao", x => x.BaseAddress = new Uri("http://localhost:5001"))
				.AddPolicyHandler(retryPolicy);

			services.AddScoped<IServicoAutenticacao, ServicoAutenticacao>();

			services.AddSingleton<IServicoComanda, ServicoComanda>();
			services.AddSingleton<IServicoProduto, ServicoProduto>();

			services.AddIdentityCore<CodeChallengeIdentityUser>()
					.AddClaimsPrincipalFactory<BearerTokenClaimFactory>();
			services.AddScoped<IUserStore<CodeChallengeIdentityUser>, CodeChallengeUserStore>();
			services.AddScoped<SignInManager<CodeChallengeIdentityUser>>();
			services.AddScoped<UserManager<CodeChallengeIdentityUser>>();
			services.AddScoped<UserValidator<CodeChallengeIdentityUser>>();
			services.AddScoped<IPasswordHasher<CodeChallengeIdentityUser>, PasswordHasher<CodeChallengeIdentityUser>>();
			services.AddHttpContextAccessor();
			services.ConfigureApplicationCookie(options =>
			{
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromHours(5);
				options.LoginPath = "/Account/Login";
				options.LogoutPath = "/Account/Logout";
				options.SlidingExpiration = true;
			});
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = "Identity.Application";
				x.DefaultChallengeScheme = "Identity.Application";
			})
				.AddCookie("Identity.Application")
				.AddCookie("Identity.External")
				.AddCookie("Identity.TwoFactorUserId");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Comanda/Error");
				app.UseHsts();
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Comanda}/{action=Index}/{id?}");
			});

			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
		}
	}
}
