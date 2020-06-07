using Dominio.Base;
using Dominio.ObjetosValor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
	public class Comanda : EntidadeBase
	{
		public List<ItemComanda> Itens { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal Desconto { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal ValorComanda { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal ValorTotal { get; set; }

		[Column(TypeName = "varchar(500")]
		public string Mensagem { get; set; }

		public Comanda()
		{
		}

		public Comanda(int id, List<ItemComanda> itens)
		{
			if (id < 1)
			{
				throw new ArgumentNullException("O Id da comanda deve ser um número maior que zero.");
			}

			Id = id;
			Itens = itens;
		}
	}
}