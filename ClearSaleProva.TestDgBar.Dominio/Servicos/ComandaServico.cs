using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using Dominio.Entidades;
using Dominio.ObjetosValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Dominio.Servicos
{
	public class ComandaServico : IComandaServico
	{
		private const int SUCO = 3;
		private readonly IComandaRepositorio _comandaRepository;
		private readonly IProdutoRepositorio _produtoRepository;

		public ComandaServico(IComandaRepositorio comandaRepository, IProdutoRepositorio produtoRepository)
		{
			_comandaRepository = comandaRepository;
			_produtoRepository = produtoRepository;
		}
		public async Task<Comanda> GetAsync(int id)
		{
			Comanda comanda = await _comandaRepository.GetAsync(id);
			return comanda;
		}

		public async Task<Resultado<Comanda>> AddAsync()
		{
			try
			{
				Comanda comanda = new Comanda();

				comanda = await _comandaRepository.AddAsync(comanda);

				return Resultado<Comanda>.Sucesso(comanda);

			}
			catch (Exception e)
			{
				return Resultado<Comanda>.Falha($"Erro ao adicionar comanda: {e.Message}");
			}
		}

		public async Task<Resultado<Comanda>> UpdateAsync(Comanda comanda)
		{
			try
			{
				Comanda temp = await _comandaRepository.GetAsync(comanda.Id);

				if (temp.Itens == null)
				{
					temp.Itens = new List<ItemComanda>();
				}

				temp.Itens.AddRange(comanda.Itens);

				await _comandaRepository.UpdateAsync(temp);
				return Resultado<Comanda>.Sucesso(temp);

			}
			catch (Exception e)
			{
				return Resultado<Comanda>.Falha($"Erro ao atualizar comanda: {e.Message}");
			}
		}

		public async Task<Resultado<Comanda>> CloseComandaAsync(Comanda comanda)
		{
			try
			{
				Comanda temp = await _comandaRepository.GetAsync(comanda.Id);

				if (temp.Itens == null)
				{
					temp.Itens = new List<ItemComanda>();
				}
				await _comandaRepository.UpdateAsync(temp);
				return Resultado<Comanda>.Sucesso(temp);

			}
			catch (Exception e)
			{
				return Resultado<Comanda>.Falha($"Erro ao fechar comanda: {e.Message}");
			}
		}

		public async Task<Resultado<Comanda>> ResetAsync(Comanda comanda)
		{
			try
			{
				Comanda temp = await _comandaRepository.GetAsync(comanda.Id);

				if (temp == null)
					return Resultado<Comanda>.Falha("Comanda não encontrada!");


				temp.Itens.Clear();

				await _comandaRepository.ResetAsync(temp);

				return Resultado<Comanda>.Sucesso(temp);
			}
			catch (Exception e)
			{
				return Resultado<Comanda>.Falha($"Erro ao resetar comanda: {e.Message}");
			}
		}

		public async Task<Resultado<Comanda>> InsertItemAsync(ItemComanda item)
		{
			try
			{
				Produto p = await _produtoRepository.GetAsync(item.Produto.Id);

				if (p == null)
				{
					return Resultado<Comanda>.Falha("Produto inválido!");
				}

				// Regra: Só é permitido 3 sucos por comanda.
				// primeiro verifica se o pedido ja supera 3 sucos
				if (item.Quantidade > 3 && item.Produto.Id == SUCO)
				{
					return Resultado<Comanda>.Falha("Não é permitido solicitar mais que 3 sucos numa mesma comanda.");
				}

				Comanda temp = await _comandaRepository.GetAsync(item.ComandaId);

				//primeiro pedido da comanda
				if (temp.Itens == null)
				{
					temp.Itens = new List<ItemComanda>();
				}
				// verifica o total da comanda (atual + pedidos anteriores)
				else if (item.Produto.Id == SUCO)
				{
					if ((temp.Itens.Where(x => x.Produto.Id == 3).Sum(x => x.Quantidade) + item.Quantidade) > 3)
					{
						return Resultado<Comanda>.Falha("Não é permitido solicitar mais que 3 sucos numa mesma comanda.");
					}
				}

				item.Produto = p;

				await _comandaRepository.InsertItemAsync(item);

				temp = await _comandaRepository.GetAsync(item.ComandaId);

				return Resultado<Comanda>.Sucesso(temp);
			}
			catch (Exception e)
			{
				return Resultado<Comanda>.Falha($"Erro ao inserir item na comanda: {e.Message}");
				throw;
			}
		}
	}
}
