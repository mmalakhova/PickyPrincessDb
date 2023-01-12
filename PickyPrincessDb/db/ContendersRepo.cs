using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.db;

public class ContendersRepo : IRepo<Contender>
{
    private readonly HallContext _context;

    public ContendersRepo(HallContext context)
    {
        _context = context;
    }

    public void Add(Contender entity) => _context.Add(entity);

    public void Add(IList<Contender> entities) => _context.AddRange(entities);

    public void Update(Contender entity) => _context.Update(entity);

    public void Update(IList<Contender> entities) => _context.UpdateRange(entities);

    public void Delete(Contender entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Contender? GetOne(int? id) => _context.Contenders.Find(id);

    public List<Contender?> GetSome(Expression<Func<Contender, bool>> where) =>
        _context.Contenders.Where(where).ToList()!;

    public List<Contender> GetAll() => _context.Contenders.ToList();

    public List<Contender> GetAll<TSortField>(Expression<Func<Contender, TSortField>> orderBy, bool ascending)
    {
        return (ascending ? _context.Contenders.OrderBy(orderBy) : _context.Contenders.OrderByDescending(orderBy))
            .ToList();
    }
}