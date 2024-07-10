namespace CDBCalculator.Domain.Entities.Cdb
{
    public class RetornoInvestimento(decimal valorBruto, decimal valorLiquido)
    {
        public decimal ValorBruto { get; private set; } = valorBruto;
        public decimal ValorLiquido { get; private set; } = valorLiquido;
    }
}
