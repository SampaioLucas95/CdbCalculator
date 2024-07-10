using CDBCalculator.Domain.Entities.Cdb;
namespace Teste.Domain.Entities.Cdb
{
    public class InvestimentoCdbTests
    {
        [Theory]
        [InlineData(1000, 6, 1059.76)] 
        [InlineData(1000, 12, 1123.08)] 
        public void CalcularValorBruto_DeveRetornarValorEsperado(decimal valorInicial, int prazoMeses, decimal valorEsperado)
        {
            // Arrange
            var investimentoCdb = new InvestimentoCdb(valorInicial, prazoMeses);

            // Act
            var valorBruto = investimentoCdb.CalcularValorBruto();

            // Assert
            Assert.Equal(valorEsperado, valorBruto, 2);
        }

        [Theory]
        [InlineData(1000, 6, 821.31)]
        [InlineData(1000, 12, 898.46)]
        public void CalcularValorLiquido_DeveRetornarValorEsperado(decimal valorInicial, int prazoMeses, decimal valorEsperado)
        {
            // Arrange
            var investimentoCdb = new InvestimentoCdb(valorInicial, prazoMeses);

            // Act
            var valorLiquido = investimentoCdb.CalcularValorLiquido();

            // Assert
            Assert.Equal(valorEsperado, valorLiquido, 2);
        }

        [Fact]
        public void CalcularInvestimentoCdb_DeveRetornarRetornoInvestimentoValido()
        {
            // Arrange
            var valorInicial = 1000m;
            var prazoMeses = 6;
            var valorLiquidoEsperado = 821.31m;
            var valorBrutoEsperado = 1059.76m;

            var investimentoCdb = new InvestimentoCdb(valorInicial, prazoMeses);
            var valorBruto = investimentoCdb.CalcularValorBruto();
            var valorLiquido = investimentoCdb.CalcularValorLiquido();
            // Act
            var retornoInvestimento = new RetornoInvestimento(valorBruto, valorLiquido);

            // Assert
            Assert.NotNull(retornoInvestimento);
            Assert.IsType<RetornoInvestimento>(retornoInvestimento);
            Assert.Equal(valorLiquidoEsperado, valorLiquido, 2);
            Assert.Equal(valorBrutoEsperado, valorBruto, 2);
        }
    }
}
