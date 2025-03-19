using Company_Expense_Tracker.Application.Caching;
using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.DepartmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DepartmentsCotroller : Controller
{
    private readonly IDepartmentService _service;
    private readonly IDistributedCache _cache;

    public DepartmentsCotroller(IDepartmentService service, IDistributedCache cache)
    {
        _service = service;
        _cache = cache;
    }

    [HttpGet]
    public async Task<ActionResult<List<Department>>> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    [Authorize(Roles = "Administrator,FinanceManager,DepartmentHead")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetById([FromRoute] int id)
    {
        string key = $"department-{id}";

        var department = await _cache.GetOrCreateAsync(key, async token =>
        {
            var department = await _service.GetByIdAsync(id);
            return department;
        });
        
        return Ok(department);
    }

    [Authorize(Roles = "Administrator,FinanceManager")]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateDepartmentDto dto)
    {
        var departmentId = await _service.CreateAsync(dto);
        return Created("",departmentId);
    }
    
    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentDto dto)
    {
        await _cache.RemoveAsync($"department-{id}");
        
        dto.Id = id;
        await _service.UpdateAsync(dto);
        
        return NoContent();
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _cache.RemoveAsync($"department-{id}");
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}