using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Services.WorkerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company_Expense_Tracker.Controllers;

[Authorize(Roles = "Administrator")]
[ApiController]
[Route("api/[controller]")]
public class WorkersController : Controller
{
    private readonly IWorkerService _service;

    public WorkersController(IWorkerService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpGet]
    public async Task<ActionResult<List<WorkerDto>>> GetAll()
    {
        var workers = await _service.GetAllAsync();
        return Ok(workers);
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkerDto>> GetById([FromRoute] int id)
    { 
        var worker = await _service.GetByIdAsync(id);
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
        dto.Id = id;
        await _service.UpdateAsync(dto);
        
        return NoContent();
    }

    [Authorize(Roles = "Administrator,DepartmentHead")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id); 
        return NoContent();
    }
}