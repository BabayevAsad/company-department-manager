using Company_Expense_Tracker.Application.Caching;
using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.WorkerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class WorkersController : Controller
{
    private readonly IWorkerService _service;
    private readonly IDistributedCache _cache;

    public WorkersController(IWorkerService service, IDistributedCache cache)
    {
        _service = service;
        _cache = cache;
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpGet]
    public async Task<ActionResult<List<Worker>>> GetAll()
    {
        var workers = await _service.GetAllAsync();
        return Ok(workers);
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Worker>> GetById([FromRoute] int id)
    {
        string key = $"worker-{id}";

        var worker = await _cache.GetOrCreateAsync(key, async token =>
        {
            var worker = await _service.GetByIdAsync(id);
            return worker;
        });
        
        return Ok(worker);
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateWorkerDto dto)
    {
        var workerId = await _service.CreateAsync(dto);
        return Created("", workerId);
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateWorkerDto dto)
    {
        await _cache.RemoveAsync($"worker-{id}");
        
        dto.Id = id;
        await _service.UpdateAsync(dto);
        
        return NoContent();
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _cache.RemoveAsync($"worker-{id}");
        await _service.DeleteAsync(id);
        
        return NoContent();
    }
}