namespace NCInterpret;

public class State
{
    public List<Reader> ProgramCallStack = new List<Reader>();
    public int ProgramCallPointer = -1;

    public int LineNumber = 1;

    public bool Alarm = false;
    public string AlarmMessage = "";

    public string CurrentProgramName()
    {
        if (ProgramCallPointer > -1)
        {
            return ProgramCallStack[ProgramCallPointer].GetProgramName();
        }
        return "None";
    }
}