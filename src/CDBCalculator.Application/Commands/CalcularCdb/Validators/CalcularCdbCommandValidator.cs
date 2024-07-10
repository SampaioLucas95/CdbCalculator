using FluentValidation;

namespace CalculoCdbApi.Application.Commands
{
    public class CalcularCdbCommandValidator : AbstractValidator<CalcularCdbCommand>
    {
        public CalcularCdbCommandValidator()
        {
            RuleFor(x => x.ValorInicial)
                .NotEmpty().WithMessage("O campo Valor Inicial é obrigatório.")
                .GreaterThan(0).WithMessage("O campo Valor Inicial deve ser maior que zero.");

            RuleFor(x => x.PrazoMeses)
                .NotEmpty().WithMessage("O campo Prazo em Meses é obrigatório.")
                .GreaterThan(1).WithMessage("O campo Prazo em Meses deve ser maior que um.");
        }
    }
}
