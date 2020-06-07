using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClearSaleProva.TestDgBar.Web.Models
{
	public class FechamentoViewModel
	{
		public int Id { get; set; }

		public List<ItemComanda> Itens { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal Desconto { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal ValorComanda { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal ValorTotal { get; set; }

		public string Mensagem { get; set; }

	}
}