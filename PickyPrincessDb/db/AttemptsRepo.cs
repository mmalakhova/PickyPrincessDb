using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.db;

public class AttemptsRepo : IRepo<Attempt>
{
    private readonly HallContext _context;

    public AttemptsRepo(HallContext context)
    {
        _context = context;
    }

    public void Add(Attempt entity) => _context.Add(entity);

    public void Add(IList<Attempt> entities) => _context.AddRange(entities);

    public void Update(Attempt entity) => _context.Update(entity);

    public void Update(IList<Attempt> entities) => _context.UpdateRange(entities);

    public void Delete(Attempt entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public Attempt? GetOne(int? id) => _context.Attempts.Find(id);

    public List<Attempt?> GetSome(Expression<Func<Attempt, bool>> where) =>
        _context.Attempts.Where(where).Include(x => x.Contenders).ToList()!;
    
    public List<Attempt> GetAll() => _context.Attempts.ToList();

    public List<Attempt> GetAll<TSortField>(Expression<Func<Attempt, TSortField>> orderBy, bool ascending)
    {
        return (ascending ? _context.Attempts.OrderBy(orderBy) : _context.Attempts.OrderByDescending(orderBy)).ToList();
    }
}