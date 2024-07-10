using CDBCalculator.Domain.Entities.Cdb;
namespace Teste.Domain.Entities.Cdb
{
    public class RetornoInvestimentoTests
    {
        [Fact]
        public void Construtor_DeveInicializarPropriedadesCorretamente()
        {
            // Arrange
            decimal valorBruto = 1000.50m;
            decimal valorLiquido = 900.75m;

            // Act
            var retornoInvestimento = new RetornoInvestimento(valorBruto, valorLiquido);

            // Assert
            Assert.Equal(valorBruto, retornoInvestimento.ValorBruto);
            Assert.Equal(valorLiquido, retornoInvestimento.ValorLiquido);
        }
    }
}
