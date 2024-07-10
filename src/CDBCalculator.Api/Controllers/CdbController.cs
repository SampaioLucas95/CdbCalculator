using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{apiVersion}/[controller]")]
public class CdbController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public CdbController(IMediator mediator, ILogger<CdbController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("calcular")]
    public async Task<ActionResult<CalcularCdbCommandResult>> CalcularCdb(CalcularCdbCommand command)
    {
        string messageBegin = $"Inicio do cálculo de Investimento CDB {DateTime.UtcNow} Utc";
        _logger.LogInformation(messageBegin);

        var result = await _mediator.Send(command);

        string messageFinesh = $"Fim do cálculo de Investimento CDB {DateTime.UtcNow} Utc";
        _logger.LogInformation(messageFinesh);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}
