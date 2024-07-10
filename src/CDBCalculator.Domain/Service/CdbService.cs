
using CDBCalculator.Domain.Entities.Cdb;

namespace CDBCalculator.Domain.Service
{
    public class CdbService : ICdbService
    {
        public RetornoInvestimento CalcularCdb(decimal valorInicial, int prazoMeses)
        {
            var investimentoCdb = new InvestimentoCdb(valorInicial, prazoMeses);
            decimal valorBruto = investimentoCdb.CalcularValorBruto();
            decimal valorLiquido = investimentoCdb.CalcularValorLiquido();

            return new RetornoInvestimento(valorBruto,valorLiquido);
        }
    }

}
