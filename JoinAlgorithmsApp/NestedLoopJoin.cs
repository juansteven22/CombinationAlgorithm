using System.Collections.Generic;

public class NestedLoopJoin
{
    public static List<(string, string)> Join(List<(string, string)> table1, List<(string, string)> table2)
    {
        var result = new List<(string, string)>();

        foreach (var (key1, value1) in table1)
        {
            foreach (var (key2, value2) in table2)
            {
                if (key1 == key2)
                {
                    result.Add((key1, $"{value1}, {value2}"));
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
    B --> C[Iterar a través de tabla1]
    C --> D[Iterar a través de tabla2]
    D --> E{¿Claves coinciden?}
    E -->|Sí| F[Agregar par al resultado]
    E -->|No| G[Siguiente elemento en tabla2]
    F --> G
    G --> H{¿Más elementos en tabla2?}
    H -->|Sí| D
    H -->|No| I[Siguiente elemento en tabla1]
    I --> J{¿Más elementos en tabla1?}
    J -->|Sí| C
    J -->|No| K[Fin]
";
    }
}