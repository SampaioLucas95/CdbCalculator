using CDBCalculator.Application.Shared;
using MediatR;

public class CalcularCdbCommand : IRequest<GenericCommandResult<CalcularCdbCommandResult>>
{
    public decimal ValorInicial { get; set; }
    public int PrazoMeses { get; set; }
}
