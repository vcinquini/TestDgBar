using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using Dominio.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Servicos
{
	public class ProdutoServico : IProdutoServico
	{
		private readonly IProdutoRepositorio _produtoRepository;

		public ProdutoServico(IProdutoRepositorio produtoRepository)
		{
			_produtoRepository = produtoRepository;
		}

		public async Task<Produto> GetAsync(int id)
		{
			try
			{
				return await _produtoRepository.GetAsync(id);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<IReadOnlyList<Produto>> GetListAsync()
		{
			try
			{
				return await _produtoRepository.GetListAsync();
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
