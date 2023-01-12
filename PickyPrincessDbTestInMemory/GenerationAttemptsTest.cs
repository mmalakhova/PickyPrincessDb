using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PickyPrincessDb.db;
using PickyPrincessDb.entities;
using PickyPrincessDb.model;

namespace PickyPrincessDbTestInMemory;

using Xunit;

public class GenerationAttemptsTest
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions _contextOptions;
    
    public GenerationAttemptsTest()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<HallContext>()
            .UseSqlite(_connection)
            .Options;

        using var context = new HallContext(_contextOptions);

        context.Database.EnsureCreated();

        context.AddRange(
            new Attempt { Name = "A1" },
            new Attempt { Name = "A2" });
        context.SaveChanges();
    }

    HallContext CreateContext() => new HallContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
    
    [Fact]
    public void GenerateAttemptWithoutError()
    {
        AttemptGenerator.GenerateAttempt("GenerateAttempt_noerror", CreateContext());
    }

    [Fact]
    public void GenerateAttemptWithCorrectName()
    {
        using var context = CreateContext();
        var repo = new AttemptsRepo(context);
        AttemptGenerator.GenerateAttempt("testattempt", context);

        Assert.Equal(4,
            repo.GetSome(a => a.Name == "testattempt").Capacity);
    }

    [Fact]
    public void GenerateAttemptWithNotEmptyContendersList()
    {
        using var context = CreateContext();
        var attempt = AttemptGenerator.GenerateAttempt("GenerateAttempt_notzerocontenders", context);

        Assert.NotEmpty(attempt.Contenders);
    }
}