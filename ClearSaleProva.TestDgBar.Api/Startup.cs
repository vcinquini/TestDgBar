using Aplicacao.Builder;
using ClearSaleProva.TestDgBar.Aplicacao.Builder;
using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using ClearSaleProva.TestDgBar.Aplicacao.Queries;
using ClearSaleProva.TestDgBar.Dominio;
using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using ClearSaleProva.TestDgBar.InfraEstrutura;
using ClearSaleProva.TestDgBar.InfraEstrutura.Repositorio;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace ClearSaleProva.TestDgBar.Api
{
	public class Startup
	{
		private readonly byte[] key = Encoding.ASCII.GetBytes(Settings.Secret);

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers()
				.AddNewtonsoftJson();
			services.AddScoped<IComandaRepositorio, ComandaRepositorio>();
			services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
			services.AddScoped<IComandaServico, ComandaServico>();
			services.AddScoped<IProdutoServico, ProdutoServico>();
			services.AddScoped<IBuilder, ConcreteBuilder>();
			services.AddScoped<IPromocoesBuilder, PromocoesBuilder>();
			services.AddScoped<IPromocoesDispatcher, PromocoesDispatcher>();

			services.AddMediatR(typeof(ListaProdutosQuery).Assembly);

			// Servico de health checks
			services.AddHealthChecks()
				// health check para o database
				.AddCheck(
					"Comanda-check",
					new SqlHealthCheck(Configuration.GetConnectionString("DefaultConnection")),
					HealthStatus.Unhealthy,
					new string[] { "comandadb" });

			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
					opt =>
					{
						opt.EnableRetryOnFailure(
							maxRetryCount: 10, 
							maxRetryDelay: TimeSpan.FromSeconds(10), 
							errorNumbersToAdd: null);
						opt.MigrationsAssembly("ClearSaleProva.TestDgBar.Infra");
					});
				options.ConfigureWarnings(
					b => b.Default(WarningBehavior.Throw)
						.Log(CoreEventId.SensitiveDataLoggingEnabledWarning)
						.Log(CoreEventId.PossibleUnintendedReferenceComparisonWarning));
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClearSaleProva.TestDgBar API", Version = "v1" });
			});

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClearSaleProva.TestDgBar API");
			});

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
				{
					ResultStatusCodes =
					{
						[HealthStatus.Healthy] = StatusCodes.Status200OK,
						[HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
					}
				}
				);
			});
		}
	}
}
