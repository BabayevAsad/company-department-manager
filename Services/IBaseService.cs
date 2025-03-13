using Company_Expense_Tracker.Dtos;

namespace Company_Expense_Tracker.Services;

public interface IBaseService<TEntity,TEntityCreateDto,TEntityUpdateDto> 
    where TEntity : BaseDto
    where TEntityCreateDto : ActionDto
    where TEntityUpdateDto : ActionDto
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<int> CreateAsync(TEntityCreateDto createDto);
    Task UpdateAsync(TEntityUpdateDto updateDto);
    Task DeleteAsync(int id);
}