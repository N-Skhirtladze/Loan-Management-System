using loan_management_system.DTOs;
using loan_management_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace loan_management_system.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class LoansController : Controller
{
    private readonly ILoansService _loansService;
    
    public LoansController(ILoansService loansService)
    {
        _loansService = loansService;
    }

    [HttpPost("CreateApplication")]
    public async Task<IActionResult> AddLoan([FromBody] LoanDto loan, int customerId)
    {
        try
        {
            var result = await  _loansService.AddLoan(loan, customerId);
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

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetLoanStatus(int Id)
    {
        try
        {
            var result = await  _loansService.GetLoanStatus(Id);
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

    [HttpGet("GetLoan/{id}")]
    public async Task<IActionResult> GetLoan(int id)
    {
        try
        {
            var result = await  _loansService.GetLoan(id);
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

    [HttpGet("GetAllLoans")]
    public async Task<IActionResult> GetAllLoans()
    {
        var result = await  _loansService.GetAllLoans();
        return Ok(result);
    }

    [HttpDelete("DeleteLoan/{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        try
        {
            await _loansService.DeleteLoan(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}