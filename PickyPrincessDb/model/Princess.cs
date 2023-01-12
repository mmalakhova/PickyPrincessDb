using PickyPrincessDb.exceptions;

namespace PickyPrincessDb.model;

public class Princess
{
    private readonly Hall _hall;
    private readonly Friend _friend;

    public Princess(Hall hall, Friend friend)
    {
        _hall = hall;
        _friend = friend;
    }

    public int ChoseHusband()
    {
        var contendersCount = _hall.GetContendersCount();
        if (_hall.CurrentContender != -1)
        {
            throw new EmptyHallException();
        }

        while (_hall.CurrentContender != contendersCount / 2)
        {
            _hall.CallNextContender();
        }

        while (_hall.CurrentContender != contendersCount - 1)
        {
            var isBetterCount = 0;
            for (var i = 0; i < _hall.CurrentContender; i++)
            {
                var friendAnswer = _friend.AskWhoBetter(i);
                if (friendAnswer)
                {
                    ++isBetterCount;
                }
            }

            if (isBetterCount >= contendersCount / 2)
            {
                return _hall.CurrentContender;
            }

            _hall.CallNextContender();
        }

        return -1;
    }
}