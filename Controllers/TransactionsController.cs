using Microsoft.AspNetCore.Mvc;
using FinanceControl.DTOs;
using FinanceControl.Services;

namespace FinanceControl.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int? categoryId)
    {
        var transactions = await _service.GetAllAsync(startDate, endDate, categoryId);
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var transaction = await _service.GetByIdAsync(id);
        return transaction is null ? NotFound() : Ok(transaction);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var summary = await _service.GetSummaryAsync(startDate, endDate);
        return Ok(summary);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TransactionCreateDTO dto)
    {
        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TransactionUpdateDTO dto)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated is null ? NotFound() : Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}