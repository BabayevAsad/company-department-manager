﻿using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Services.DepartmentService;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<DepartmentDto>>> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    [Authorize(Roles = "Administrator,FinanceManager,DepartmentHead")]
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDetailsDto>> GetById([FromRoute] int id)
    { 
        var department = await _service.GetByIdAsync(id);
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
        dto.Id = id;
        await _service.UpdateAsync(dto);
        
        return NoContent();
    }
    
    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}