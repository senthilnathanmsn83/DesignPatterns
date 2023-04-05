using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static System.Console;


public class Journal
{
    private readonly List<string> entries = new List<string>();
    private static int count = 0;
    public int AddEntry(string text)
    {
        entries.Add($"{++count} : {text}");
        return count;
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }

    public void Save(string filename)
    {
        File.WriteAllText(filename, ToString());
    }

    public static Journal Load(string filename)
    {
        return new Journal();
    }

    public void Load(Uri uri)
    {

    }
}

public class Persistance
{
    public void SaveToFile(Journal j, string filename, bool overwrite = false)
    {
        if (overwrite || !File.Exists(filename))
            File.WriteAllText(filename, j.ToString());
    }
}


public class Demo
{
    public static void Main(string[] args)
    {
        var j = new Journal();
        j.AddEntry("I cried today");
        j.AddEntry("I ate a bug");
        WriteLine(j);

        var p = new Persistance();
        var filename = @"c:\aws\testfile.txt";
        p.SaveToFile(j, filename, true);

        ProcessStartInfo psi = new ProcessStartInfo(filename);
        psi.UseShellExecute = true;
        Process.Start(psi);

    }
}