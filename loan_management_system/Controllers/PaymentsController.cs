using loan_management_system.DTOs;
using loan_management_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace loan_management_system.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _paymentsService;

    public PaymentsController(IPaymentsService paymentsService)
    {
        _paymentsService = paymentsService;
    }

    [HttpPost("{loanId}")]
    public async Task<IActionResult> Payment([FromBody] PaymentsDto paymentsDto, int loanId)
    {
        try
        {
            var result = await _paymentsService.Payment(paymentsDto, loanId);
            return Ok(result);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message, stack = e.StackTrace });  
        }
    }
}