using loan_management_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace loan_management_system.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class LoanScheduleController : ControllerBase
{
    private readonly ILoanScheduleService _loanScheduleService;

    public LoanScheduleController(ILoanScheduleService loanScheduleService)
    {
        _loanScheduleService = loanScheduleService;
    }

    [HttpGet("LoanSchedule/{id}")]
    public async Task<IActionResult> GetLoanSchedule(int id)
    {
        try
        {
            var result = await  _loanScheduleService.GetLoanSchedules(id);
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