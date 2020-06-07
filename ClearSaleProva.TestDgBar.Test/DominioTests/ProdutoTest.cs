using Dominio.ObjetosValor;
using FluentAssertions;
using System;
using Xunit;

namespace ClearSaleProva.TestDgBar.DominioTests
{
	public class ProdutoTest
	{
		[Fact]
		public void Construtor_Deve_LancarArgumentException_Quando_Id_Menor_Zero()
		{
			Action action = () => new Produto(0);

			action
				.Should()
				.Throw<ArgumentNullException>();
		}


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Construtor_Deve_LancarArgumentException_Quando_Descricao_Vazio_Null(string desc)
		{
			Action action = () => new Produto(1, desc, 1, 1);
			action
				.Should()
				.Throw<ArgumentNullException>();
		}

		[Fact]
		public void Construtor_Deve_LancarArgumentException_Quando_Preco_MenorIgual_Zero()
		{
			Action action = () => new Produto(1, "Pipoca", 0, 1);
			action
				.Should()
				.Throw<ArgumentNullException>();
		}

		[Fact]
		public void Construtor_NaoDeve_LancarArgumentException_Quando_Dados_Valido()
		{
			Produto produto = new Produto(5, "Pinga", 1, 1);

			produto
				.Should()
				.NotBeNull();

			produto
				.Id
				.Should()
				.Equals(5);

			produto
				.Descricao
				.Should()
				.Equals("Pinga");

			produto
				.Preco
				.Should()
				.Equals(1);

			produto
				.PrecPromocional
				.Should()
				.Equals(1);
		}

		[Fact]
		public void Produtos_Equal_Quando_Dados_Iguais()
		{
			Produto p1 = new Produto(5, "Pinga", 1, 1);
			Produto p2 = new Produto(5, "Pinga", 1, 1);

			p1
				.Should()
				.BeEquivalentTo(p2);
			p1
				.Should()
				.NotBeSameAs(p2);

		}
	}

}