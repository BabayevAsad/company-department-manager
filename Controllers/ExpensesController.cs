using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.ExpenseService;
using Microsoft.AspNetCore.Mvc;

namespace Company_Expense_Tracker.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ExpensesController : Controller
{
    private readonly IExpenseService _service;

    public ExpensesController(IExpenseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetAll()
    {
        var expenses = await _service.GetAllAsync();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetById([FromRoute] int id)
    {
        var expense = await _service.GetByIdAsync(id);
        return Ok(expense);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateExpenseDto dto)
    {
        var expenseId = await _service.CreateAsync(dto);
        return Created("", expenseId);
    }

    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateExpenseDto dto)
    {
        dto.Id = id;
        await _service.UpdateAsync(dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}