using Dominio.Entidades;
using Dominio.ObjetosValor;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClearSaleProva.TestDgBar.Test
{
	static class Helper
	{
		public static List<ItemComanda> CriarLista(int q1, int q2, int q3, int q4)
		{
			var cerveja = new Produto(1, "Cerveja", 5, 3);
			var conhaque = new Produto(2, "Conhaque", 20, 20);
			var suco = new Produto(3, "Suco", 50, 50);
			var agua = new Produto(4, "Agua", 70, 70);

			return new List<ItemComanda>()
			{
				new ItemComanda(){ ComandaId = 1, Id = 1, Produto = cerveja, Quantidade = q1 },
				new ItemComanda(){ ComandaId = 1, Id = 2, Produto = conhaque, Quantidade = q2 },
				new ItemComanda(){ ComandaId = 1, Id = 3, Produto = suco, Quantidade = q3 },
				new ItemComanda(){ ComandaId = 1, Id = 4, Produto = agua, Quantidade = q4 }
			};
		}

		public static Task<List<Produto>> ObterLista()
		{
			return Task.FromResult(new List<Produto>()
			{
				new Produto(1, "Cerveja", 5, 3),
				new Produto(2, "Conhaque", 20, 20),
				new Produto(2, "Suco", 50, 50),
				new Produto(2, "Agua", 70, 70)
			});
		}

		public static Task<Comanda> ObterComanda(int id) =>
		   Task.FromResult(new Comanda(id, Helper.CriarLista(1, 1, 1, 1)));

		public static Task<Produto> ObterProduto() =>
		   Task.FromResult(new Produto(1, "Cerveja", 5, 3));

		public static ItemComanda ObterItem(int comandaId, int q1) =>
			new ItemComanda(comandaId, new Produto(3, "Suco", 50, 50), q1);


	}
}
