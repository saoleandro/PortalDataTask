using Microsoft.EntityFrameworkCore;
using PortalDataTask.Domain.Entities;
using PortalDataTask.Domain.Interfaces.Repositories;

namespace PortalDataTask.Infra.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly ContextDb _contextDb;

    public UserRepository(ContextDb contextDb)
    {
        _contextDb = contextDb;
    }

    public void Dispose()
    {
        _contextDb.Dispose();
    }


    public async Task<User> GetAsync(string login)
    {
        return await _contextDb.Users!.FirstOrDefaultAsync(f => f.Login == login);
    }
}
