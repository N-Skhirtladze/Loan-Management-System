using loan_management_system.DTOs;
using loan_management_system.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace loan_management_system.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomersController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomers([FromBody] CustomersDto customerDto)
    {
        try
        {
            var result = await _customersService.AddCustomers(customerDto);
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

    [HttpGet("loans")]
    public async Task<IActionResult> GetLoansByCustomerId([FromQuery] int customerId)
    {
        try
        {
            var result = await _customersService.GetLoansByCustomerId(customerId);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message, stack = e.StackTrace });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] int customerId)
    {
        try
        {
            var result = await _customersService.GetCustomer(customerId);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = e.Message, stack = e.StackTrace });
        }
    }

    [HttpGet("AllCustomers")]
    public async Task<IActionResult> GetAllCustomers()
    {
        var result = await _customersService.GetAllCustomers();
        return Ok(result);
    }

    [HttpDelete("DeleteCustomer/{customerId}")]
    public async Task<IActionResult> DeleteCustomers(int customerId)
    {
        try
        {
            await _customersService.DeleteCustomer(customerId);
            return NoContent(); 
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}