using NCInterpret;

namespace Program;

public static class Program
{
    public static void Main(string[] args)
    {
        // State testState = new State();

        // Reader fileReader = new Reader("test_wrong.nc");
        // fileReader.ReadFile(ref testState);

        // if (testState.Alarm)
        // {
        //     Console.WriteLine(testState.AlarmMessage);
        // }

        State testState = new State()
        {
            ProgramCallPointer = 0
        };

        testState.ProgramCallStack.Add(new Reader("test.nc"));
        testState.ProgramCallStack[0].ReadFile(ref testState);

        Console.WriteLine(testState.ProgramCallStack[0]);
        Console.WriteLine();

        Console.WriteLine("'{0}'", testState.CurrentProgramName());
    }
}