using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PickyPrincessDb.db;
using PickyPrincessDb.entities;
using PickyPrincessDb.model;

namespace PickyPrincessDbTestInMemory;

using Xunit;

public class SimulationAttemptsTest
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions _contextOptions;
    
    public SimulationAttemptsTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<HallContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new HallContext(_contextOptions);
        context.Database.EnsureCreated();
        context.Add(new Attempt { Name = "at1", Count = 0 });
        context.SaveChanges();
    }

    HallContext CreateContext() => new HallContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
    
    [Fact]
    public void ReadingAttempt()
    {
        using var context = CreateContext();
        var repo = new AttemptsRepo(context);
        
        var attempt = repo.GetSome(a => a.Name == "at1");
        Assert.Equal(0, attempt[0]!.Count);

    }
    
    [Fact]
    public void ExecutingAttempt()
    {
        using var context = CreateContext();
        Attempt attempt = AttemptGenerator.GenerateAttempt("testattempt", context);
        PrincessSimulator.Simulate(attempt);
    }
}