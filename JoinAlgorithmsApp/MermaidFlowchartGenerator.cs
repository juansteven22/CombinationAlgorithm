using System.IO;
using System.Threading.Tasks;

public class MermaidFlowchartGenerator
{
    public static Task GenerateFlowchart(string algorithmName, string flowchartDefinition)
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Flowcharts");
        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, $"{algorithmName}_flowchart.mmd");
        return File.WriteAllTextAsync(filePath, flowchartDefinition);
    }
}
