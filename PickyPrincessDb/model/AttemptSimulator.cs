using PickyPrincessDb.db;
using PickyPrincessDb.entities;

namespace PickyPrincessDb.model;

public class AttemptSimulator
{
    private int _currentContender;

    public Attempt Attempt { get; set; }

    public AttemptSimulator(string attemptName)
    {

        var attemptsRepo = new AttemptsRepo(new HallContext());
        var attempts = attemptsRepo.GetSome(attempt => attempt.Name == attemptName);
        if (attempts == null || attempts.Count == 0)
        {
            throw new Exception("Attempt does not exist!");
        }

        Attempt = attempts[0]!;
    }
    
}