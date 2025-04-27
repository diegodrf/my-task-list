using MyTaskList.Application.Entities;

namespace MyTaskList.Application.Abstractions.Ports.Driver;

public interface IItemService
{
    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Item>> GetAllAsync(CancellationToken cancellationToken);
    Task<Item> CreateAsync(string name, CancellationToken cancellationToken);
    Task<Item> UpdateAsyncAsync(Guid id, string name, bool done, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}