using ClearSaleProva.TestDgBar.Dominio.Repositorio;
using ClearSaleProva.TestDgBar.Dominio.Servicos;
using Dominio.Entidades;
using Dominio.ObjetosValor;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace ClearSaleProva.TestDgBar.Test.RepositorioTests
{
	public class ProdutoRepositorioTest
	{
		[Fact]
		public async void Repository_Produto_GetAsync_ReturnsProduto()
		{
			var subsProdutoRepo = Substitute.For<IProdutoRepositorio>();

			subsProdutoRepo.GetAsync(1).Returns(Helper.ObterProduto());

			IProdutoServico servico = new ProdutoServico(subsProdutoRepo);

			var ret = await servico.GetAsync(1);

			ret.Should()
				.NotBeNull();

			ret.Id
				.Should()
				.Equals(1);
		}

		[Fact]
		public async void Repository_Produto_GetListAsync_ReturnsListaProduto()
		{
			var subsProdutoRepo = Substitute.For<IProdutoRepositorio>();

			subsProdutoRepo.GetListAsync().Returns(Helper.ObterLista());

			IProdutoServico servico = new ProdutoServico(subsProdutoRepo);

			var ret = await servico.GetListAsync();

			ret.Should()
				.NotBeNull();

			ret.Should()
				.HaveCount(4);

		}

		[Fact]
		public async void Repository_Comanda_CloseAsync_ReturnsComanda()
		{
			var subsComandaRepo = Substitute.For<IComandaRepositorio>();
			var subsProdutoRepo = Substitute.For<IProdutoRepositorio>();

			subsComandaRepo.GetAsync(3).Returns(Helper.ObterComanda(3));

			IComandaServico servico = new ComandaServico(subsComandaRepo, subsProdutoRepo);

			Comanda atual = await servico.GetAsync(3);

			var ret = await servico.UpdateAsync(atual);

			ret.Value
				.Id
				.Should()
				.Equals(3);

			ret.Value
				.Itens
				.Should()
				.NotBeEmpty();
		}

		[Fact]
		public async void Repository_Comanda_ResetAsync_ReturnsComanda_SemItens()
		{
			var subsComandaRepo = Substitute.For<IComandaRepositorio>();
			var subsProdutoRepo = Substitute.For<IProdutoRepositorio>();

			subsComandaRepo.GetAsync(3).Returns(Helper.ObterComanda(3));

			IComandaServico servico = new ComandaServico(subsComandaRepo, subsProdutoRepo);

			Comanda atual = await servico.GetAsync(3);

			var ret = await servico.ResetAsync(atual);

			ret.Value
				.Id
				.Should()
				.Equals(3);

			ret.Value
				.Itens
				.Should()
				.BeEmpty();
		}

		[Fact]
		public async void Repository_Comanda_InsertItemAsync_ReturnsComanda_ComItem()
		{
			var subsComandaRepo = Substitute.For<IComandaRepositorio>();
			var subsProdutoRepo = Substitute.For<IProdutoRepositorio>();

			subsComandaRepo.GetAsync(3).Returns(Helper.ObterComanda(3));
			subsProdutoRepo.GetAsync(3).Returns(new Produto(2, "Suco", 50, 50));

			IComandaServico servico = new ComandaServico(subsComandaRepo, subsProdutoRepo);

			ItemComanda item = Helper.ObterItem(3, 2);

			var ret = await servico.InsertItemAsync(item);

			ret.Value
				.Id
				.Should()
				.Equals(3);

			ret.Value
				.Itens
				.Should()
				.HaveCount(4);
		}
	}
}
