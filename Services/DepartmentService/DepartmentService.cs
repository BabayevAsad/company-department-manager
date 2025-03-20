using Company_Expense_Tracker.Application.Caching;
using Company_Expense_Tracker.Dtos.DepartmentDtos;
using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Services.DepartmentService;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly IDistributedCache _cache;

    public DepartmentService(IDepartmentRepository repository, IDistributedCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repository.GetAllAsync();

        var dto = departments.Where(d => !d.IsDeleted)
            .Select(d => new DepartmentDto()
            {   
                Id = d.Id,
                Name = d.Name,
            }).ToList();

        return dto;
    }

    public async Task<DepartmentDetailsDto> GetByIdAsync(int id)
    {
        string key = $"{nameof(Department)}-{id}";

        var department = await _cache.GetOrCreateAsync(key, async token =>
        {
            var department = await _repository.GetByIdAsync(id);
            var dto = new DepartmentDetailsDto()
            {
                Id = department.Id,
                Name = department.Name,
                Expenses = department.Expenses.Select(e => new ExpenseDto()
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Currency = e.Currency,
                    PaymentMethod = e.PaymentMethod,
                    Catagory = e.Catagory,
                    DepartmentId = e.DepartmentId,
                    Description = e.Description
                }).ToList(),
                Workers = department.Workers.Select(w => new WorkerDto()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Surname = w.Surname,
                    FatherName = w.FatherName,
                    BirthDate = w.BirthDate,
                    Email = w.Email,
                    PhoneNumber = w.PhoneNumber,
                    Nationality = w.Nationality,
                    FinNumber = w.FinNumber,
                    GenderId = (Gender)GenderHelper.GetById(w.GenderId),
                    DepartmentId = w.DepartmentId
                }).ToList()
            };
            
            return dto;
        });
        
        return department;
    }

    public async Task<int> CreateAsync(CreateDepartmentDto createDto)
    {
        var department = new Department()
        {
            Name = createDto.Name,
        };
        
       await _repository.CreateAsync(department);
       return department.Id;
    }

    public async Task UpdateAsync(UpdateDepartmentDto updateDto)
    {
        await _cache.RemoveAsync($"{nameof(Department)}-{updateDto.Id}");
        var department = await _repository.GetByIdAsync(updateDto.Id);
      
        department.Name = updateDto.Name;
        
        await _repository.UpdateAsync(department);
    }

    public async Task DeleteAsync(int id)
    {
        await _cache.RemoveAsync($"{nameof(Department)}-{id}");

        var department = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(department);
    }
}