using Dominio.Entidades;
using FluentAssertions;
using System;
using Xunit;

namespace ClearSaleProva.TestDgBar.DominioTests
{
	public class ComandaTest
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void Construtor_Deve_LancarArgumentException_Quando_Id_Menor_Um(string id)
		{
			Action action = () => new Comanda(id: 0, itens: null);

			action
				.Should()
				.Throw<ArgumentNullException>();
		}


		[Fact]
		public void Construtor_NaoDeve_LancarArgumentException_Quando_Id_Valido()
		{
			Comanda comanda = new Comanda(id: 1, itens: null);

			comanda
				.Should()
				.NotBeNull();

			comanda
				.Id
				.Should()
				.Equals(1);
		}
	}
}
