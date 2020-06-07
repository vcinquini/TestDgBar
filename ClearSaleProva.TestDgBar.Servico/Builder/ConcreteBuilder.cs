using ClearSaleProva.TestDgBar.Aplicacao.Interfaces;
using Dominio.ObjetosValor;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearSaleProva.TestDgBar.Aplicacao.Builder
{
	public class ConcreteBuilder : IBuilder
	{
		private NotaFiscal _nf = new NotaFiscal();

		public List<ItemComanda> Lista { get; set; }
		public NotaFiscal NotaFiscal { get { return _nf; } }


		public ConcreteBuilder()
		{
		}

		// Calcula todos os itens da lista antes de aplica promoçoes
		public void CalcularNf()
		{
			if (Lista != null && Lista.Any())
			{
				_nf.ValorComanda = Lista.Sum(x => x.Quantidade * x.Produto.Preco);
			}
		}

		public void PromocaoCervejaSuco()
		{
			// obtem o preco promocional da cerveja
			decimal prpromo = Lista.Where(x => x.Produto.Id == 1).Select(x => x.Produto.PrecPromocional).FirstOrDefault();

			if (prpromo > 0)  // so calcula se tiver preco promocional cadastrado
			{
				// obter o total de cervejas
				int cervejas = Lista.Where(x => x.Produto.Id == 1).Sum(x => x.Quantidade);
				// obter o total de aguas
				int sucos = Lista.Where(x => x.Produto.Id == 3).Sum(x => x.Quantidade);

				// pegar o minimo dos dois
				int diff = Math.Min(cervejas, sucos);

				// sera o valor das cervejas a 3,00
				decimal vlCervejaEmPromo = prpromo * diff;

				// acrescentar no campo desconto (se houver)
				if (vlCervejaEmPromo > 0)
				{
					_nf.Desconto = vlCervejaEmPromo;
					_nf.Mensagem += $"Você economizou {vlCervejaEmPromo.ToString("C")}";
				}
			}
		}

		public void PromocaoConhaqueCerveja()
		{
			int conhaques = Lista.Where(x => x.Produto.Id == 2).Sum(x => x.Quantidade);
			int cervejas = Lista.Where(x => x.Produto.Id == 1).Sum(x => x.Quantidade);

			conhaques /= 3;
			cervejas /= 2;

			int premio = Math.Min(conhaques, cervejas);
			if (premio > 0)
				_nf.Mensagem += $"\nParabéns! Você ganhou {premio} água(s)";
		}
	}
}
