using System.Collections.Generic;

public class IndexNestedLoopJoin
{
    public static List<(string, string)> Join(List<(string, string)> table1, List<(string, string)> table2)
    {
        var result = new List<(string, string)>();
        var index = new SortedDictionary<string, List<string>>();

        foreach (var (key, value) in table2)
        {
            if (!index.ContainsKey(key))
                index[key] = new List<string>();
            index[key].Add(value);
        }

        foreach (var (key, value) in table1)
        {
            if (index.TryGetValue(key, out var matches))
            {
                foreach (var match in matches)
                {
                    result.Add((key, $"{value}, {match}"));
                }
            }
        }

        return result;
    }

    public static string GetFlowchartDefinition()
    {
        return @"
graph TD
    A[Inicio] --> B[Crear lista de resultados vacía]
    B --> C[Crear índice para tabla2]
    C --> D[Iterar a través de tabla1]
    D --> E{¿Clave de tabla1 en índice?}
    E -->|Sí| F[Obtener coincidencias del índice]
    E -->|No| G[Siguiente elemento en tabla1]
    F --> H[Agregar pares coincidentes al resultado]
    H --> G
    G --> I{¿Más elementos en tabla1?}
    I -->|Sí| D
    I -->|No| J[Fin]
";
    }
}