using System;

namespace ClearSaleProva.TestDgBar.Dominio
{
	public class Resultado
	{
		public bool Ok { get; }
		public string CodigoErro { get; }

		protected Resultado(bool sucesso, string codigoErro)
		{
			if (!sucesso && String.IsNullOrEmpty(codigoErro))
				throw new ArgumentException("O código de erro precisa ser definido", nameof(codigoErro));

			Ok = sucesso;
			CodigoErro = codigoErro;
		}

		public static Resultado Sucesso() => new Resultado(true, null);
		public static Resultado Falha(string codigoErro) => new Resultado(false, codigoErro);
	}

	public /*sealed*/ class Resultado<T> : Resultado
	{
		public T Value { get; }

		private Resultado(bool sucesso, T value, string codigoErro) : base(sucesso, codigoErro)
		{
			if (sucesso && Equals(value, default(T)))
				throw new ArgumentException("O valor necessita ser definido", nameof(value));

			Value = value;
		}

		public static Resultado<T> Sucesso(T value) => new Resultado<T>(true, value, null);

		public static new Resultado<T> Falha(string errorCode) => new Resultado<T>(false, default, errorCode);
	}
}
