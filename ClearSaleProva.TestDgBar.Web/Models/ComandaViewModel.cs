using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClearSaleProva.TestDgBar.Web.Models
{
	public class ComandaViewModel
	{
		public int Id { get; set; }

		[Required]
		[Range(1, Int32.MaxValue, ErrorMessage = "Selecione um produto")]
		public int ItemId { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Por favor entre com um valor maior que 0")]
		public int Quantidade { get; set; }

		public List<ItemComanda> Itens { get; set; }

		public SelectList Produtos { get; set; }

		public string Mensagem { get; set; }

	}

	public class ItemComanda
	{
		public int ComandaId { get; set; }

		public int Id { get; set; }

		public Produto Produto { get; set; }

		public int Quantidade { get; set; }
	}

	public class Produto
	{
		public int Id { get; set; }

		public string Descricao { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal Preco { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
		public decimal PrecPromocional { get; set; }
	}


}