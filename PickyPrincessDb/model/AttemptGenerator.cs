using PickyPrincessDb.db;
using PickyPrincessDb.entities;
using PickyPrincessDb.exceptions;

namespace PickyPrincessDb.model;

public static class AttemptGenerator
{
    private static Hall _hall;
    private static List<Contender> _contenders;
    private static ContendersRepo _contendersRepo;
    private static AttemptsRepo _attemptRepo;
    private static Attempt _attempt;
    private static HallContext _context;
    
    public static Attempt GenerateAttempt(string name, HallContext context)
    {
        Console.WriteLine("ATTEMPT GENERATOR");

        _attempt = new Attempt
        {
            Name = name, Count = 100,
            Contenders = new List<Contender>()
        };
        _context = context;
        _attemptRepo = new AttemptsRepo(_context);
        _contendersRepo = new ContendersRepo(_context);

        _contenders = ContendersGenerator.GenerateFromInternet(100);
        
        var hall = new Hall(_contenders);
        _hall = hall;
        var friend = new Friend(_contenders, _hall);

        var contendersCount = _hall.GetContendersCount();
        if (_hall.CurrentContender != -1)
        {
            throw new EmptyHallException();
        }

        while (_hall.CurrentContender != contendersCount / 2)
        {
            _hall.CallNextContender();
            SaveContender(_hall.CurrentContender);
        }

        var chosenIdx = -1;
        while (_hall.CurrentContender != contendersCount - 1)
        {
            var isBetterCount = 0;
            for (var i = 0; i < _hall.CurrentContender; i++)
            {
                var friendAnswer = friend.AskWhoBetter(i);
                if (friendAnswer)
                {
                    ++isBetterCount;
                }
            }
            if (isBetterCount >= contendersCount / 2)
            {
                chosenIdx = _hall.CurrentContender;
                SaveContender(chosenIdx);
                break;
            }

            _hall.CallNextContender();
            SaveContender(_hall.CurrentContender);
        }
        int happyLevel;
        if (chosenIdx == -1)
        {
            happyLevel = 10;
        }
        else if (_contenders[chosenIdx].Rating <= 50)
        {
            happyLevel = 0;
        }
        else
        {
            happyLevel = _contenders[chosenIdx].Rating;
        }

        _attempt.HappyLevel = happyLevel;
        _attemptRepo.Add(_attempt);
        _context.SaveChanges();
        Console.WriteLine($"Name = {name}, Happy Level = {happyLevel}");

        return _attempt;
    }

    private static void SaveContender(int idx)
    {
        var contender = _contenders[_hall.CurrentContender];
        contender.Attempt = _attempt;
        contender.OrderIdx = idx;
        _contendersRepo.Add(contender);
        _attempt.Contenders.Add(contender);
    }
}