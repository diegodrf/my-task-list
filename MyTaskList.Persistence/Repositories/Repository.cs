using Microsoft.EntityFrameworkCore;
using MyTaskList.Application.Abstractions.Ports.Driven;
using MyTaskList.Application.Entities;

namespace MyTaskList.Persistence.Repositories;

public class Repository : IRepository
{
    private readonly MyTaskListDbContext _db;

    public Repository(MyTaskListDbContext db)
    {
        _db = db;
    }
    
    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Items.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Item>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Items.ToListAsync(cancellationToken);
    }

    public async Task SaveAsync(Item item, CancellationToken cancellationToken)
    {
        if (_db.Entry(item).State == EntityState.Detached)
        {
            await _db.Items.AddAsync(item, cancellationToken);
        }

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _db.Items
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}