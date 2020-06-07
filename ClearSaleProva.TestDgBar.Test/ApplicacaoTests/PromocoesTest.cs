using Aplicacao.Builder;
using ClearSaleProva.TestDgBar.Aplicacao.Builder;
using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using ClearSaleProva.TestDgBar.Test;
using FluentAssertions;
using Xunit;

namespace ClearSaleProva.TestDgBar.AplicacaoTests
{
	public class PromocoesTest
	{
		public PromocoesTest()
		{
		}

		[Fact]
		public void Builder_Promocao_1Cerveja1Suco()
		{
			// Arrange
			IBuilder builder = new ConcreteBuilder();
			IPromocoesDispatcher dispatcher = new PromocoesDispatcher(builder);

			// Act
			dispatcher.PromocoesDispatch(Helper.CriarLista(1, 0, 1, 0));

			// Assert
			builder.NotaFiscal
				   .Desconto
				   .Should()
				   .Equals(3);

			builder.NotaFiscal
				   .ValorComanda
				   .Should()
				   .Equals(55);

			builder.NotaFiscal
				   .ValorTotal
				   .Should()
				   .Equals(52);
		}

		[Fact]
		public void Builder_Promocao_3Conhaques2Cervejas()
		{
			// Arrange
			IBuilder builder = new ConcreteBuilder();
			IPromocoesDispatcher dispatcher = new PromocoesDispatcher(builder);

			// Act
			dispatcher.PromocoesDispatch(Helper.CriarLista(2, 3, 0, 0));

			// Assert
			builder.NotaFiscal
				   .Desconto
				   .Should()
				   .Equals(0);

			builder.NotaFiscal
				   .ValorComanda
				   .Should()
				   .Equals(70);

			builder.NotaFiscal
				   .ValorTotal
				   .Should()
				   .Equals(70);

			builder.NotaFiscal
				   .Mensagem
				   .Should()
				   .Equals("\nParabéns! Você ganhou 1 água(s)");

		}

	}

}
