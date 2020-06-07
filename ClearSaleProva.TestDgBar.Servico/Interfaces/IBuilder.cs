using ClearSaleProva.TestDgBar.Aplicacao.Builder;
using Dominio.ObjetosValor;
using System.Collections.Generic;

namespace ClearSaleProva.TestDgBar.Aplicacao.Interfaces
{
	public interface IBuilder
	{
		List<ItemComanda> Lista { get; set; }
		NotaFiscal NotaFiscal { get; }

		void CalcularNf();
		void PromocaoCervejaSuco();
		void PromocaoConhaqueCerveja();
	}
}
