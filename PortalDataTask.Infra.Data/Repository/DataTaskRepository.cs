
using PortalDataTask.Domain.Entities;
using PortalDataTask.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using PortalDataTask.Domain.Enums;

namespace PortalDataTask.Infra.Data.Repository;

public class DataTaskRepository : IDataTaskRepository
{
    private readonly ContextDb _contextDb;

    public DataTaskRepository(ContextDb contextDb)
    {
        _contextDb = contextDb;
    }

    public void Dispose()
    {
        _contextDb.Dispose();
    }


    public async Task<List<DataTask>> GetAllAsync()
    {
        return await _contextDb.DataTasks!.ToListAsync();
    }

    public async Task<DataTask> GetByIdAsync(long id)
    {
        return await _contextDb.DataTasks!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<DataTask> GetByDescriptionAsync(string description)
    {
        return await _contextDb.DataTasks!.FirstOrDefaultAsync(f => f.Description.ToLower() == description.ToLower());
    }

    public async Task<List<DataTask>> GetByFilters(string description, DateTime? validateDate, DateTime? finalDate, StatusEnum? status)
    {
        var filteredData = _contextDb.DataTasks!;

        if (validateDate.HasValue)
        {
            if (!finalDate.HasValue)
                finalDate = new DateTime(validateDate.Value.Year, validateDate.Value.Month, validateDate.Value.Day, 23,59,59);
            else
                finalDate = new DateTime(finalDate.Value.Year, finalDate.Value.Month, finalDate.Value.Day, 23, 59, 59);
        }

        return await _contextDb.DataTasks!.Where(f =>
                    (!string.IsNullOrWhiteSpace(description) && (f.Description.StartsWith(description))) ||
                    (validateDate.HasValue && (f.ValidateDate >= validateDate && f.ValidateDate <= finalDate)) ||
                    (status.HasValue && (f.Status == status)))
                    .ToListAsync();
    }

    public async Task CreateAsync(DataTask dataTask)
    {
        await _contextDb.DataTasks!.AddAsync(dataTask);
        await _contextDb.SaveChangesAsync();
    }

    public async Task UpdateAsync(DataTask dataTask)
    {
        _contextDb.DataTasks!.Update(dataTask);
        await _contextDb.SaveChangesAsync();
    }
}
