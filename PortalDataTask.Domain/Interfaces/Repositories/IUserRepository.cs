using PortalDataTask.Domain.Entities;

namespace PortalDataTask.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetAsync(string login);
}
