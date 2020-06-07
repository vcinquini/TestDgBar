using Dominio.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.ObjetosValor
{
	public class ItemComanda : ValueObject<ItemComanda>
	{
		public int Id { get; set; }

		[ForeignKey("Comanda")]
		public int ComandaId { get; set; }

		public Produto Produto { get; set; }

		public int Quantidade { get; set; }

		public ItemComanda()
		{
		}

		public ItemComanda(int comandaId, Produto item, int quantidade)
		{
			if (comandaId < 1)
			{
				throw new ArgumentNullException("O Id da comanda deve ser um número maior que zero.");
			}

			if (quantidade < 1)
			{
				throw new ArgumentNullException("O Quantidade do produto deve ser um número maior que zero.");
			}

			if (item == null)
			{
				throw new ArgumentNullException("Produto não pode ser nulo.");
			}

			ComandaId = comandaId;
			Produto = item;
			Quantidade = quantidade;
		}

		public override bool EqualsCore(ItemComanda other)
		{
			return (Id == other.Id);
		}

		public override int GetHashCodeCore()
		{
			return $"{Id}{Quantidade}{Produto.Id}{Produto.Descricao}{Produto.Preco}".GetHashCode();
		}
	}
}
