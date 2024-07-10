using CDBCalculator.Application.Shared;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Teste.Controllers
{
    public class CdbControllerTests
    {
        [Fact]
        public async Task CalcularCdb_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var command = new CalcularCdbCommand { ValorInicial = 1000, PrazoMeses = 12 };
            var cancellationToken = CancellationToken.None;
            var expectedResult = new GenericCommandResult<CalcularCdbCommandResult>(true, new CalcularCdbCommandResult
            {
                ValorBruto = 1100,
                ValorLiquido = 1050
            });

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(command, cancellationToken))
                        .ReturnsAsync(expectedResult);

            var controller = new CdbController(mediatorMock.Object);

            // Act
            var result = await controller.CalcularCdb(command);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var resultValue = Assert.IsType<GenericCommandResult<CalcularCdbCommandResult>>(okResult.Value);
            Assert.Equal(expectedResult.Data.ValorBruto, resultValue.Data.ValorBruto);
            Assert.Equal(expectedResult.Data.ValorLiquido, resultValue.Data.ValorLiquido);
        }

        [Fact]
        public async Task CalcularCdb_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new CalcularCdbCommand { ValorInicial = -100, PrazoMeses = 0 }; 
            var cancellationToken = CancellationToken.None;
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("ValorInicial", "ValorInicial deve ser maior que zero"),
                new ValidationFailure("PrazoMeses", "PrazoMeses deve ser maior que zero")
            });

            var mediatorMock = new Mock<IMediator>();
            var loggerMock = new Mock<ILogger<CdbController>>();
            mediatorMock.Setup(x => x.Send(command, cancellationToken))
                        .ReturnsAsync(new GenericCommandResult<CalcularCdbCommandResult>(
                false, new CalcularCdbCommandResult(), validationResult.Errors.Select(e => e.ErrorMessage).ToList()));

            var controller = new CdbController(mediatorMock.Object, loggerMock.Object);

            // Act
            var result = await controller.CalcularCdb(command);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            var commandResult = Assert.IsType<GenericCommandResult<CalcularCdbCommandResult>>(badRequestResult.Value);
            Assert.Equal(2, commandResult.Errors?.Count);
        }
    }
}
