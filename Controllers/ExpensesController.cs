using Company_Expense_Tracker.Application.Caching;
using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.ExpenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ExpensesController : Controller
{
    private readonly IExpenseService _service;
    private readonly IDistributedCache _cache;
    public ExpensesController(IExpenseService service, IDistributedCache cache)
    {
        _service = service;
        _cache = cache;
    }
    
    [Authorize(Roles = "Administrator,FinanceManager,DepartmentHead")]
    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetAll()
    {
        var expenses = await _service.GetAllAsync();
        return Ok(expenses);
    }
    
    [Authorize(Roles = "Administrator,FinanceManager")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetById([FromRoute] int id)
    {
        string key = $"expense-{id}";

        var expense = await _cache.GetOrCreateAsync(key, async token =>
        {
            var expense = await _service.GetByIdAsync(id);
            return expense;
        });
        
        return Ok(expense);
    }
    
    [Authorize(Roles = "Administrator,FinanceManager,DepartmentHead")]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateExpenseDto dto)
    {
        var expenseId = await _service.CreateAsync(dto);
        return Created("", expenseId);
    }
    
    [Authorize(Roles = "Administrator,FinanceManager")]
    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateExpenseDto dto)
    {
        await _cache.RemoveAsync($"expense-{id}");
        
        dto.Id = id;
        await _service.UpdateAsync(dto);
        
        return NoContent();
    }

    [Authorize(Roles = "Administrator,FinanceManager")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _cache.RemoveAsync($"expense-{id}");
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}