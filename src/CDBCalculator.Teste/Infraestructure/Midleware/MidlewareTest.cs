using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using CDBCalculator.Infrastructure.Midleware;

namespace Teste.Infrastructure.Midleware
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task Invoke_ExceptionThrown_CapturesAndHandlesException()
        {
            // Arrange
            var requestDelegateMock = new Mock<RequestDelegate>();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(requestDelegateMock.Object, loggerMock.Object);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            var expectedException = new Exception("Test exception");
            requestDelegateMock.Setup(rd => rd(It.IsAny<HttpContext>())).ThrowsAsync(expectedException);

            // Act
            await middleware.Invoke(context);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var responseBody = await reader.ReadToEndAsync();

            Assert.Contains("Ocorreu um erro inesperado. Tente novamente mais tarde.", responseBody);
            Assert.Equal("application/json", context.Response.ContentType);
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        }
    }

}
