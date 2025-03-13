using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Services.DepartmentService;
using Microsoft.AspNetCore.Mvc;

namespace Company_Expense_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]

public class DepartmentsCotroller : Controller
{
    private readonly IDepartmentService _service;

    public DepartmentsCotroller(IDepartmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Department>>> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetById([FromRoute] int id)
    {
        var book = await _service.GetByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateDepartmentDto dto)
    {
        var departmentId = await _service.CreateAsync(dto);
        return Created("",departmentId);
    }

    [HttpPut("{id}")]
    public async Task<NoContentResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentDto dto)
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