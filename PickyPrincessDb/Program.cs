using NDesk.Options;
using PickyPrincessDb.db;
using PickyPrincessDb.model;

namespace PickyPrincessDb;

public class Program
{
    private enum ProgramMode
    {
        SimulationAll,
        SimulationSome,
        GeneratingAttempts
    }


    public static void Main()
    {
        var programMode = ProgramMode.SimulationAll;
        var attemptNames = new List<string>();
        attemptNames.Add("one");
        attemptNames.Add("two");

        RunProgram(programMode, attemptNames);
    }

    private static void RunProgram(ProgramMode programMode, List<string> attemptsNames)
    {
        switch (programMode)
        {
            case ProgramMode.SimulationAll:
                PrincessSimulator.SimulateAll();
                break;

            case ProgramMode.SimulationSome:
                foreach (var attempt in attemptsNames)
                {
                    PrincessSimulator.SimulateBehavior(attempt);
                }

                break;

            case ProgramMode.GeneratingAttempts:
                foreach (var attempt in attemptsNames)
                {
                    AttemptGenerator.GenerateAttempt(attempt, new HallContext());
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}