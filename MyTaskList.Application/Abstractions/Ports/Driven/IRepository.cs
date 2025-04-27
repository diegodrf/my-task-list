using MyTaskList.Application.Entities;

namespace MyTaskList.Application.Abstractions.Ports.Driven;

public interface IRepository
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task SaveAsync(Item item, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}