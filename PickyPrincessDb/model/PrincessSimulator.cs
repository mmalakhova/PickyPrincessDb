using PickyPrincessDb.db;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.model;

public static class PrincessSimulator
{
    private static readonly string Delimiter = new('-', 10);

    public static void SimulateBehavior(string attemptName)
    { 
        var attemptsRepo = new AttemptsRepo(new HallContext());
        var attempts = attemptsRepo.GetSome(attempt => attempt.Name == attemptName);
        var attempt = attempts[0];
        Simulate(attempt);
    }

    public static void SimulateAll()
    {
        var repo = new AttemptsRepo(new HallContext());
        var attempts = repo.GetAll();
        Console.WriteLine("Got attempts");
        Console.WriteLine(attempts[0]!.Contenders.Count);
        Console.WriteLine(attempts.Count);
        attempts.ForEach(Simulate);
        var averageHappy = attempts.Sum(a => a.HappyLevel) / attempts.Count;
        Console.WriteLine($"Average Happy Level = {averageHappy}");
    }

    public static void Simulate(Attempt attempt)
    {
        Console.WriteLine("--Princess Behavior simulation--");
        Console.WriteLine("Attempt name: " + attempt.Name);
        Console.WriteLine("Contenders count: " + attempt.Contenders.Count);
        var contenders = attempt.Contenders;
        contenders.Sort((c1, c2) => c1.OrderIdx - c2.OrderIdx);
        foreach (var c in contenders)
        {
            Console.WriteLine($"{c.OrderIdx}){c.Name}:{c.Rating}");
        }
        Console.WriteLine($"Happy level: {attempt.HappyLevel}");
        
        Console.WriteLine(Delimiter);
    }
}