using PickyPrincessDb.entities;
using PickyPrincessDb.exceptions;

namespace PickyPrincessDb.model;

public class Hall
{
    public List<Contender> Contenders { get; }

    public int CurrentContender { get; private set; }

    public Hall(List<Contender> contenders)
    {
        Contenders = contenders;
        CurrentContender = -1;
    }

    public int GetContendersCount()
    {
        return Contenders.Count;
    }

    public void CallNextContender()
    {
        ++CurrentContender;
        if (CurrentContender >= Contenders.Count)
        {
            throw new EmptyHallException();
        }
    }
}