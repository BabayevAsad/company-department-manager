using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Repositories;

namespace Company_Expense_Tracker.Services.DepartmentService;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repository.GetAllAsync();

        var dto = departments.Where(d => !d.IsDeleted)
            .Select(d => new DepartmentDto
            {   
                Id = d.Id,
                Name = d.Name,
                DepartmentId = d.DepartmentId
            }).ToList();

        return dto;
    }

    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);

        var dto = new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            DepartmentId = department.DepartmentId
        };

        return dto;
    }

    public async Task<int> CreateAsync(CreateDepartmentDto createDto)
    {
        var department = new Department()
        {
            Name = createDto.Name,
            DepartmentId = createDto.DepartmentId
        };
        
       await _repository.CreateAsync(department);
       return department.Id;
    }

    public async Task UpdateAsync(UpdateDepartmentDto updateDto)
    {
        var department = await _repository.GetByIdAsync(updateDto.Id);

        department.Name = updateDto.Name;
        department.DepartmentId = updateDto.DepartmentId;

        await _repository.UpdateAsync(department);
    }

    public async Task DeleteAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(department);
    }
}