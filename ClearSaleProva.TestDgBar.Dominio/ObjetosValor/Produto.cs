using Dominio.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.ObjetosValor
{
	public class Produto : ValueObject<Produto>
	{
		public int Id { get; set; }

		[Column(TypeName = "varchar(50)")]
		public string Descricao { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal Preco { get; set; }

		[Column(TypeName = "decimal(18,4)")]
		public decimal PrecPromocional { get; set; }

		public Produto()
		{
		}

		public Produto(int id)
		{
			if (id < 1)
			{
				throw new ArgumentNullException("O Id do produto deve ser um número maior que zero.");
			}

			Id = id;
		}

		public Produto(int id, string descricao, decimal preco, decimal precPromocional)
		{
			if (id < 1)
			{
				throw new ArgumentNullException("O Id do produto deve ser um número maior que zero.");
			}

			if (String.IsNullOrEmpty(descricao))
			{
				throw new ArgumentNullException("Descrição do produto inválida.");
			}

			if (preco < 1)
			{
				throw new ArgumentNullException("Preço do produto inválida.");
			}

			Id = id;
			Descricao = descricao;
			Preco = preco;
			PrecPromocional = precPromocional;
		}

		public override bool EqualsCore(Produto other)
		{
			return (Id == other.Id);
		}

		public override int GetHashCodeCore()
		{
			return $"{Id}{Descricao}{Preco}".GetHashCode();
		}
	}
}