namespace CDBCalculator.Domain.Entities.Cdb
{
    public class InvestimentoCdb
    {
        public decimal ValorInicial { get; private set; }
        public int PrazoMeses { get; private set; }

        private const decimal TaxaBanco = 1.08m; // 108%
        private const decimal TaxaCdi = 0.009m; // 0.9%

        public InvestimentoCdb(decimal valorInicial, int prazoMeses)
        {
            ValorInicial = valorInicial;
            PrazoMeses = prazoMeses;
        }

        public decimal CalcularValorBruto()
        {
            decimal taxaAcumulada = 1 + (TaxaCdi * TaxaBanco);
            decimal valorBruto = ValorInicial;

            for (int i = 0; i < PrazoMeses; i++)
                valorBruto *= taxaAcumulada;

            return Math.Round(valorBruto, 2);
        }

        public decimal CalcularValorLiquido()
        {
            decimal valorBruto = CalcularValorBruto();
            decimal imposto = CalcularImposto();

            decimal valorLiquido = valorBruto * (1 - imposto);
            return Math.Round(valorLiquido, 2);
        }

        private decimal CalcularImposto()
        {
            if (PrazoMeses <= 6) return 0.225m;
            if (PrazoMeses <= 12) return 0.20m;
            if (PrazoMeses <= 24) return 0.175m;
            return 0.15m;
        }
    }
}


