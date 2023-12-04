namespace NCInterpret;

public class Machine
{
    State MachineState;
    string RootProgram;

    int FPS = 60;

    public Machine()
    {
        MachineState = new State();
        RootProgram = "";
    }

    public bool Reset()
    {
        MachineState = new State();
        return true;
    }

    public bool SoftReset()
    {
        MachineState = new State();
        return LoadProgram(RootProgram);
    }

    public bool LoadProgram(string _fileName)
    {
        RootProgram = _fileName;
        MachineState.ProgramCallPointer++;
        MachineState.ProgramCallStack[MachineState.ProgramCallPointer] = new Reader(_fileName);

        return MachineState.ProgramCallStack[MachineState.ProgramCallPointer].GetRead();
    }

}