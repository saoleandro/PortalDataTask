using PortalDataTask.Domain.Entities;
using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Domain.Interfaces.Repositories;

public interface IDataTaskRepository
{
    Task<List<Entities.DataTask>> GetAllAsync();
    Task<Entities.DataTask> GetByIdAsync(long id);
    Task<DataTask> GetByDescriptionAsync(string description);
    Task<List<DataTask>> GetByFilters(string description, DateTime? validateDate, DateTime? finalDate, StatusEnum? status);
    Task CreateAsync(Entities.DataTask dataTask);
    Task UpdateAsync(Entities.DataTask dataTask);
}
