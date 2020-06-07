using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using Dominio.Entidades;
using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.InfraEstrutura.Repositorio
{
	public sealed class ComandaRepositorio : IComandaRepositorio
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger _logger;

		public ComandaRepositorio(ApplicationDbContext dbContext, ILogger<ComandaRepositorio> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<Comanda> GetAsync(int id) => await _dbContext.Comandas.Include(x => x.Itens)
															.ThenInclude(y => y.Produto).FirstOrDefaultAsync(i => i.Id == id);


		public async Task<Comanda> AddAsync(Comanda comanda)
		{
			_logger.LogInformation("AddAsync iniciado");

			var novaComanda = await _dbContext.Comandas.AddAsync(comanda);
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("AddAsync encerrado");
			return novaComanda.Entity;
		}

		public async Task UpdateAsync(Comanda comanda)
		{
			_logger.LogInformation("UpdateAsync iniciado");

			_dbContext.Entry(comanda).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			_logger.LogInformation("UpdateAsync encerrado");
		}

		public async Task InsertItemAsync(ItemComanda item)
		{
			_logger.LogInformation("InsertItemAsync iniciado");

			await _dbContext.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO ItemComanda (ComandaId, ProdutoId, Quantidade) VALUES ({item.ComandaId}, {item.Produto.Id}, {item.Quantidade})");
			await _dbContext.Entry(item).ReloadAsync();

			_logger.LogInformation("InsertItemAsync encerrado");
		}

		public async Task ResetAsync(Comanda comanda)
		{
			_logger.LogInformation("ResetAsync iniciado");

			await _dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM ItemComanda WHERE ComandaId = {comanda.Id}");
			await _dbContext.Entry(comanda).ReloadAsync();

			_logger.LogInformation("ResetAsync encerrado");
		}

	}
}
