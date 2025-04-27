using MyTaskList.Application.Abstractions.Ports.Driven;
using MyTaskList.Application.Abstractions.Ports.Driver;
using MyTaskList.Application.Entities;

namespace MyTaskList.Application.Services;

public class ItemService : IItemService
{
    private readonly IRepository _repository;

    public ItemService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<Item> CreateAsync(string name, CancellationToken cancellationToken)
    {
        var item = new Item { Name = name };
        await _repository.SaveAsync(item, cancellationToken);
        return item;
    }

    public async Task<Item> UpdateAsyncAsync(Guid id, string name, bool done, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken)
                   ?? throw new ArgumentNullException($"Item {id} not found.");

        item.Name = name;
        item.Done = done;

        await _repository.SaveAsync(item, cancellationToken);

        return item;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken)
                   ?? throw new ArgumentNullException($"Item {id} not found.");
        await _repository.DeleteAsync(item.Id, cancellationToken);
    }
}