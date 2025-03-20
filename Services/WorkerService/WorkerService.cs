using Company_Expense_Tracker.Application.Caching;
using Company_Expense_Tracker.Dtos.WorkerDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Company_Expense_Tracker.Services.WorkerService;

public class WorkerService : IWorkerService
{
    private readonly IWorkerRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IDistributedCache _cache;


    public WorkerService(IWorkerRepository repository, IDepartmentRepository departmentRepository, IDistributedCache cache)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
        _cache = cache;
    }

    public async Task<List<WorkerDto>> GetAllAsync()
    {
        var workers = await _repository.GetAllAsync();

        var dto = workers.Where(w => !w.IsDeleted)
            .Select(w => new WorkerDto
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
                GenderId = (Gender)(GenderHelper.GetById(w.GenderId)),
                DepartmentId = w.DepartmentId
            }).ToList();

        return dto;
    }

    public async Task<WorkerDto> GetByIdAsync(int id)
    {
        string key = $"{nameof(Worker)}-{id}";

        var worker = await _cache.GetOrCreateAsync(key, async token =>
        {
            var worker = await _repository.GetByIdAsync(id);

            var dto = new WorkerDto
            {
                Id = worker.Id,
                Name = worker.Name,
                Surname = worker.Surname,
                FatherName = worker.FatherName,
                BirthDate = worker.BirthDate,
                Email = worker.Email,
                PhoneNumber = worker.PhoneNumber,
                Nationality = worker.Nationality,
                FinNumber = worker.FinNumber,
                GenderId = (Gender)worker.GenderId,
                DepartmentId = worker.DepartmentId
            };

            return dto;
        });
        
        return worker;
    }

    public async Task<int> CreateAsync(CreateWorkerDto createDto)
    {
        var departament = await _departmentRepository.GetByIdAsync(createDto.DepartmentId);
     
        var worker = new Worker()
        {
            Name = createDto.Name,
            Surname = createDto.Surname,
            FatherName = createDto.FatherName,
            BirthDate = createDto.BirthDate,
            Email = createDto.Email,
            PhoneNumber = createDto.PhoneNumber,
            Nationality = createDto.Nationality,
            FinNumber = createDto.FinNumber,
            GenderId = createDto.GenderId,
            DepartmentId = createDto.DepartmentId
        };
        
        await _repository.CreateAsync(worker);
        return worker.Id;
    }

    public async Task UpdateAsync(UpdateWorkerDto updateDto)
    {
        await _cache.RemoveAsync($"{nameof(Worker)}-{updateDto.Id}");
        
        var worker = await _repository.GetByIdAsync(updateDto.Id);
        var departament = await _departmentRepository.GetByIdAsync(updateDto.DepartmentId);

        worker.Name = updateDto.Name;
        worker.Surname = updateDto.Surname;
        worker.FatherName = updateDto.FatherName;
        worker.BirthDate = updateDto.BirthDate;
        worker.Email = updateDto.Email;
        worker.PhoneNumber = updateDto.PhoneNumber;
        worker.Nationality = updateDto.Nationality;
        worker.FinNumber = updateDto.FinNumber;
        worker.GenderId = updateDto.GenderId;
        worker.DepartmentId = updateDto.DepartmentId;

        await _repository.UpdateAsync(worker);
    }

    public async Task DeleteAsync(int id)
    {
        await _cache.RemoveAsync($"{nameof(Worker)}-{id}");
        
        var worker = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(worker);
    }
}