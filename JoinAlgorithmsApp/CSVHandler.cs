using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CSVHandler
{
    public static List<(string, string)> ReadCSV(string filename)
    {
        return File.ReadAllLines(filename)
            .Select(line => line.Split(','))
            .Where(parts => parts.Length == 2)
            .Select(parts => (parts[0].Trim(), parts[1].Trim()))
            .ToList();
    }

    public static void WriteCSV(string filename, List<(string, string)> data)
    {
        File.WriteAllLines(filename, data.Select(item => $"{item.Item1},{item.Item2}"));
    }
}