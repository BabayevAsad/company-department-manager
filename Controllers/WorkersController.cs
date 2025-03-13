using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.WorkerService;
using Microsoft.AspNetCore.Mvc;

namespace Company_Expense_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkersController : Controller
{
    private readonly IWorkerService _service;

    public WorkersController(IWorkerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Worker>>> GetAll()
    {
        var workers = await _service.GetAllAsync();
        return Ok(workers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Worker>> GetById([FromRoute] int id)
    {
        var worker = await _service.GetByIdAsync(id);
        return Ok(worker);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateWorkerDto dto)
    {
        var workerId = await _service.CreateAsync(dto);
        return Created("", workerId);
    }

    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateWorkerDto dto)
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