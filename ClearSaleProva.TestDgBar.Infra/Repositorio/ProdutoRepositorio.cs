using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.InfraEstrutura.Repositorio
{
	public sealed class ProdutoRepositorio : IProdutoRepositorio
	{
		private readonly ApplicationDbContext _dbContext;

		public ProdutoRepositorio(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Produto> GetAsync(int id) => await _dbContext.Produtos.AsNoTracking<Produto>().FirstOrDefaultAsync(X => X.Id == id);

		public async Task<List<Produto>> GetListAsync() => await _dbContext.Produtos.ToListAsync();

	}
}
