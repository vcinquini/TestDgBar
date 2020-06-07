namespace ClearSaleProva.TestDgBar.Aplicacao.Builder
{
	public class NotaFiscal
	{
		public NotaFiscal()
		{
			Mensagem = "";
		}
		public decimal Desconto { get; set; }

		public decimal ValorComanda { get; set; }

		public decimal ValorTotal { get { return ValorComanda - Desconto; } }

		public string Mensagem { get; set; }
	}
}