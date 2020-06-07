using Dominio.ObjetosValor;
using FluentAssertions;
using System;
using Xunit;

namespace ClearSaleProva.TestDgBar.DominioTests
{
	public class ItemComandaTest
	{
		[Theory]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		public void Construtor_Deve_LancarArgumentException_Quando_Id_Quantidade_Menor_Zero(int id, int qtd)
		{
			Produto p = new Produto(3, "Suco", 50, 50);
			Action action = () => new ItemComanda(id, p, qtd);
			action
				.Should()
				.Throw<ArgumentNullException>();
		}

		[Fact]
		public void Construtor_Deve_LancarArgumentException_Quando_Produto_e_Nulo()
		{
			Action action = () => new ItemComanda(1, null, 1);
			action
				.Should()
				.Throw<ArgumentNullException>();
		}

		[Fact]
		public void Construtor_NaoDeve_LancarArgumentException_Quando_Dados_Validos()
		{
			Produto produto = new Produto(5, "Pinga", 1, 1);
			ItemComanda item = new ItemComanda(1, produto, 1);

			item
				.Should()
				.NotBeNull();

			item
				.Id
				.Should()
				.Equals(1);

			item
				.Quantidade
				.Should()
				.Equals(1);

			item
				.Produto
				.Should()
				.BeEquivalentTo(produto);
		}

		[Fact]
		public void ItemComanda_Equal_Quando_Dados_Iguais()
		{
			Produto produto = new Produto(5, "Pinga", 1, 1);
			ItemComanda i1 = new ItemComanda(5, produto, 1);
			ItemComanda i2 = new ItemComanda(5, produto, 1);

			i1
				.Should()
				.BeEquivalentTo(i2);
			i1
				.Should()
				.NotBeSameAs(i2);

		}
	}

}