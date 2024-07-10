using CDBCalculator.Application.Shared;
using CDBCalculator.Domain.Service;
using FluentValidation;
using MediatR;

public class CalcularCdbCommandHandler : IRequestHandler<CalcularCdbCommand, GenericCommandResult<CalcularCdbCommandResult>>
{
    private readonly IValidator<CalcularCdbCommand> _validator;
    private readonly ICdbService _cdbService;

    public CalcularCdbCommandHandler(
        IValidator<CalcularCdbCommand> validator,
        ICdbService cdbService)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _cdbService = cdbService ?? throw new ArgumentNullException(nameof(cdbService));
    }

    public async Task<GenericCommandResult<CalcularCdbCommandResult>> Handle(CalcularCdbCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new GenericCommandResult<CalcularCdbCommandResult>(
                false, new CalcularCdbCommandResult(), validationResult.Errors.Select(e => e.ErrorMessage).ToList());

        var retornoInvestimentoCdb = _cdbService.CalcularCdb(request.ValorInicial, request.PrazoMeses);

        return new GenericCommandResult<CalcularCdbCommandResult>(true, new CalcularCdbCommandResult
        {
            ValorBruto = retornoInvestimentoCdb.ValorBruto,
            ValorLiquido = retornoInvestimentoCdb.ValorLiquido
        });
    }
}
