using CDBCalculator.Domain.Entities.Cdb;

namespace CDBCalculator.Domain.Service
{
    public interface ICdbService
    {
        RetornoInvestimento CalcularCdb(decimal valorInicial, int prazoMeses);
    }
}

