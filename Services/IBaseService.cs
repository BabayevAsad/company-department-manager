using Company_Expense_Tracker.Dtos;

namespace Company_Expense_Tracker.Services;

public interface IBaseService<TEntity,TDetailEntity,TEntityCreateDto,TEntityUpdateDto> 
    where TEntity : BaseDto
    where TDetailEntity : BaseDto
    where TEntityCreateDto : ActionDto
    where TEntityUpdateDto : ActionDto
{
    Task<List<TEntity>> GetAllAsync();
    Task<TDetailEntity> GetByIdAsync(int id);
    Task<int> CreateAsync(TEntityCreateDto createDto);
    Task UpdateAsync(TEntityUpdateDto updateDto);
    Task DeleteAsync(int id);
}