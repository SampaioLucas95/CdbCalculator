using CDBCalculator.Domain.Entities.Cdb;
using CDBCalculator.Domain.Service;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace CDBCalculator.Teste.CommandHandlers
{
    public class CalcularCdbCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ReturnsCalcularCdbCommandResult()
        {
            // Arrange
            var request = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 12 };
            var cancellationToken = CancellationToken.None;
            decimal valorBruto = 1100, valorLiquido = 1050;

            var cdbServiceMock = new Mock<ICdbService>();
            cdbServiceMock.Setup(x => x.CalcularCdb(It.IsAny<decimal>(), It.IsAny<int>()))
                          .Returns(new RetornoInvestimento(valorBruto,valorLiquido));

            var validatorMock = new Mock<IValidator<CalcularCdbCommand>>();
            validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CalcularCdbCommand>(), cancellationToken))
                         .ReturnsAsync(new ValidationResult());

            var handler = new CalcularCdbCommandHandler(validatorMock.Object, cdbServiceMock.Object);

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1100, result.Data.ValorBruto);
            Assert.Equal(1050, result.Data.ValorLiquido);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsValidationErrors()
        {
            // Arrange
            var request = new CalcularCdbCommand { ValorInicial = -100, PrazoMeses = 0 };
            var cancellationToken = CancellationToken.None;

            var validatorMock = new Mock<IValidator<CalcularCdbCommand>>();
            validatorMock.Setup(x => x.ValidateAsync(It.IsAny<CalcularCdbCommand>(), cancellationToken))
                         .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("ValorInicial", "O valor inicial deve ser maior que zero.") }));

            var handler = new CalcularCdbCommandHandler(validatorMock.Object, Mock.Of<ICdbService>());

            // Act
            var result = await handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
        }
    }
}

