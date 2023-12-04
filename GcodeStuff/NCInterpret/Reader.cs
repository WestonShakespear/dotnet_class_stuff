namespace NCInterpret;
public class Reader
{
    private string FileName = "";
    private List<string> FileData = new List<string>();
    private bool Read = false;
    private string ProgramName;

    public Reader(string _fileName)
    {
        FileName = _fileName;
        ProgramName = "";
    }

    public bool ReadFile(ref State _state)
    {
        if (!File.Exists(FileName))
        {
            _state.Alarm = true;
            _state.AlarmMessage = "File not found";
            return false;
        }
        
        string[] all_file = File.ReadAllLines(FileName);

        foreach(string line in all_file)
        {
            FileData.Add(line);
        }
        
        if (!CheckFile())
        {
            _state.Alarm = true;
            _state.AlarmMessage = "File structure incoherent";
            return false;
        }

        return true;

    }

    public bool CheckFile()
    {
        if (FileData.Count == 0) return false;

        if (FileData[0][0] != '%') return false;
        if (FileData[FileData.Count-1][0] != '%') return false;

        bool ProgramNameFound = false;

        foreach (string line in FileData)
        {
            if (line[0] == 'O')
            {
                ProgramNameFound = true;
                ProgramName = line;
                break;
            }
        }

        if (!ProgramNameFound) return false;

        return true;
    }

    public List<string> GetData()
    {
        return FileData;
    }
    public bool GetRead()
    {
        return Read;
    }

    public string GetProgramName()
    {
        return ProgramName;
    }

    public override string ToString()
    {
        string code = "";
        int l = 1;

        foreach (string line in FileData)
        {
            code += l.ToString();
            code += "|\t";
            code += line;
            code += ";\r\n";
            l++;
        }
        return code;
    }
}
