using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var algorithms = new List<(string Name, Func<List<(string, string)>, List<(string, string)>, List<(string, string)>> Function, Func<string> FlowchartDefinition)>
        {
            ("Hash Join", HashJoin.Join, HashJoin.GetFlowchartDefinition),
            ("Nested Loop Join", NestedLoopJoin.Join, NestedLoopJoin.GetFlowchartDefinition),
            ("Merge Join (Sort-Merge)", (t1, t2) => MergeJoin.Join(t1, t2, false), MergeJoin.GetFlowchartDefinition),
            ("Merge Join (ordenadas)", (t1, t2) => MergeJoin.Join(t1, t2, true), MergeJoin.GetFlowchartDefinition),
            ("Block Nested Loop Join", (t1, t2) => BlockNestedLoopJoin.Join(t1, t2, 1000), BlockNestedLoopJoin.GetFlowchartDefinition),
            ("Index Nested Loop Join", IndexNestedLoopJoin.Join, IndexNestedLoopJoin.GetFlowchartDefinition)
        };

        var table1 = CSVHandler.ReadCSV("input1.csv");
        var table2 = CSVHandler.ReadCSV("input2.csv");

        foreach (var (name, function, flowchartDefinition) in algorithms)
        {
            Console.Write($"{name,-30}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = function(table1, table2);

            stopwatch.Stop();

            Console.WriteLine($"{stopwatch.Elapsed.TotalSeconds:F15} segundos");

            string outputFileName = $"output_{name.Replace(" ", "_").ToLower()}.csv";
            CSVHandler.WriteCSV(outputFileName, result);

            try
            {
                await MermaidFlowchartGenerator.GenerateFlowchart(name.Replace(" ", "_"), flowchartDefinition());
                Console.WriteLine($"Diagrama de flujo generado para {name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el diagrama de flujo para {name}: {ex.Message}");
            }
        }

        Console.WriteLine("Todos los algoritmos han sido ejecutados. Los resultados y diagramas de flujo han sido guardados.");
    }
}